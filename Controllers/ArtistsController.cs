using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Data;
using MovieAppAPI.DTOs.Artist;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [Authorize(Policy="UserOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly IMapper mapper;
        public ArtistsController(IAppRepository appRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.appRepository = appRepository;

        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist(Artist artist)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            appRepository.Add(artist);
            if (await appRepository.SaveAll())
            {
            var activity = mapper.Map<ArtistActivityLog>(artist);
            activity.ArtistId = artist.Id;
            activity.AddedByUserId = userId;
            appRepository.Add(activity);
            if (await appRepository.SaveAll())
            {
                return CreatedAtRoute("GetArtist", new { id = artist.Id }, artist);
            }
            return BadRequest("Unable to Add Artist");
            }
            return BadRequest("Unable to Add Artist");
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetArtist")]
        public async Task<IActionResult> GetArtist(int id)
        {
            var artist = await appRepository.GetArtist(id);
            if (artist !=null && User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userMovieRoles = await appRepository.GetUserArtistMovieRoles(id, userId);
                artist.MovieRoles.Concat(userMovieRoles).ToList();
                var userArtistPhotos = await appRepository.GetUserArtistPhotos(id, userId);
                artist.ArtistPhotos.Concat(userArtistPhotos).ToList();
            }
            if (artist == null && User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                artist = await appRepository.GetArtist(id, userId);
            }
            if (artist == null)
            {
                return BadRequest("The artist does not exist");
            }
            var artistsToReturn = mapper.Map<ArtistForDetailDTO>(artist);
            return Ok(artistsToReturn);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetArtists([FromQuery]QueryParams queryParams)
        {
            var artists = new PaginatedList<Artist>();
            if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                artists = await appRepository.GetArtists(userId, queryParams);
            }
            else
            {
              artists = await appRepository.GetArtists(queryParams);
            }
            Response.AddPaginationHeader(artists.PageNumber, artists.PageSize, artists.TotalPages, artists.TotalItems);
            var artistsToReturn = mapper.Map<IEnumerable<ArtistForListDTO>>(artists);
            return Ok(artistsToReturn);
        }

        [HttpPost("deleteRequest/{id}")]
        public async Task<IActionResult> DeleteArtist(int id, ArtistDeleteRequest deleteRequest)
        {
            var artist = await appRepository.GetArtist(id);
            if (artist == null)
            {
                return BadRequest("The artist does not exist");
            }
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (deleteRequest.UserId != userId)
            {
                return Unauthorized();
            }
            appRepository.Add(deleteRequest);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to send delete request");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArtist(ArtistActivityLog artistActivity)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var artistFromRepo =await appRepository.GetArtist(artistActivity.ArtistId);
            if (artistFromRepo == null)
            {
                artistFromRepo = await appRepository.GetArtist(artistActivity.ArtistId, userId);
            }
            if (artistFromRepo == null)
            {
                return BadRequest("Artist does not exist.");
            }
            if (!artistFromRepo.IsApproved)
            {
                var firstActivity = await appRepository.GetArtistActivity(artistFromRepo.ActivityLogs.FirstOrDefault().Id);
                mapper.Map(artistActivity, artistFromRepo);
                mapper.Map(artistFromRepo, firstActivity);
                if (await appRepository.SaveAll())
                {
                    var artistToReturn = mapper.Map<ArtistForDetailDTO>(artistFromRepo);
                    return Ok(artistToReturn);
                }
                return BadRequest("Unable to Update Artist");
            }
            artistActivity.IsArtistApproved = true;
            artistActivity.AddedByUserId = userId;
            appRepository.Add(artistActivity);
            if (await appRepository.SaveAll())
            {
                var artistToReturn = mapper.Map<ArtistForDetailDTO>(artistFromRepo);
                return Ok(artistToReturn);
            }
            return BadRequest("Unable to Update Artist");
        }

        [AllowAnonymous]
        [HttpGet("{id}/activity")]
        public async Task<IActionResult> GetArtistActivity(int id)
        {
            var activityLog = await appRepository.GetArtistActivityLog(id);
            var activityLogToRetrun = mapper.Map<List<ArtistActivityToReturnDTO>>(activityLog);
            return Ok(activityLogToRetrun);
        }

    }
}