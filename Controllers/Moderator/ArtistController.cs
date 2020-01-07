using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieAppAPI.Data;
using MovieAppAPI.DTOs.Artist;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers.Moderator
{
    [Authorize(Policy = "ModOnly")]
    [Route("api/moderator/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly Cloudinary cloudinary;
        private readonly IMapper mapper;
        private readonly IOptions<CloudinaryCredentials> cloudinaryCredentials;
        private readonly IModeratorRepository moderatorRepository;
        public ArtistController(IModeratorRepository moderatorRepository, IAppRepository appRepository, IMapper mapper,
        IOptions<CloudinaryCredentials> cloudinaryCredentials)
        {
            this.moderatorRepository = moderatorRepository;
            this.cloudinaryCredentials = cloudinaryCredentials;
            this.mapper = mapper;
            this.appRepository = appRepository;

            cloudinary = new Cloudinary(new Account(cloudinaryCredentials.Value.CloudName, cloudinaryCredentials.Value.ApiKey,
                                        cloudinaryCredentials.Value.ApiSecret));

        }
        
        [HttpGet()]
        public async Task<IActionResult> GetArtistsToReview()
        {
            var artists = await moderatorRepository.GetArtistsToReview();
            var artistsToReturn = mapper.Map<List<ArtistForReviewDTO>>(artists);
            return Ok(artistsToReturn);
        }

        [HttpGet("{artistId}")]
        public async Task<IActionResult> GetArtistToReview(int artistId)
        {
            var artist = await moderatorRepository.GetArtistToReview(artistId);
            return Ok(artist);
        }

        [HttpPost("review/{artistId}/{isApproved}")]
        public async Task<IActionResult> ReviewArtist(int artistId, bool isApproved)
        {
            var artistToReview = await moderatorRepository.GetArtistToReview(artistId);
            if (isApproved)
            {
                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                artistToReview.IsApproved = true;
                var firstActivity = artistToReview.ActivityLogs.FirstOrDefault();
                firstActivity.ApprovedByModeratorId = modId;
                firstActivity.IsApproved = true;
                foreach (var movieRole in artistToReview.MovieRoles)
                {
                    movieRole.IsArtistApproved = true;
                }
                foreach (var activity in artistToReview.ActivityLogs)
                {
                    activity.IsArtistApproved = true;
                }
                foreach (var photo in artistToReview.ArtistPhotos)
                {
                    photo.IsArtistApproved = true;
                }
                if (await appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Unable to approve artist");
            }
            else
            {
                if (await DeleteArtist(artistToReview.Id))
                {
                    return Ok();
                }
                return BadRequest("Unable to reject artist");
            }
        }

        [HttpGet("activity")]
        public async Task<IActionResult> GetArtistActivityLogToReview()
        {
            var artistActivities = await moderatorRepository.GetArtistActivityLogToReview();
            var activityToReturn = mapper.Map<List<ArtistActivityToReturnDTO>>(artistActivities);
            return Ok(activityToReturn);
        }


        [HttpPost("activity/{artistActivityId}/{isApproved}")]
        public async Task<IActionResult> ReviewArtistActivity(int artistActivityId, bool isApproved)
        {
            var artistActivity = await moderatorRepository.GetArtistActivityToReview(artistActivityId);
            if (isApproved)
            {
                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                artistActivity.ApprovedByModeratorId = modId;
                var artist = await appRepository.GetArtist(artistActivity.ArtistId);
                artistActivity.IsApproved = true;
                var dateOfBirth = artist.DateOfBirth;
                mapper.Map(artistActivity, artist);
                if (artistActivity.DateOfBirth == null)
                {
                    artist.DateOfBirth = dateOfBirth;
                }
                if (await appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Unable to save changes");
            }
            appRepository.Delete(artistActivity);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to save changes");
        }

        [HttpGet("deleteRequests")]
        public async Task<IActionResult> GetArtistDeleteRequests()
        {
            var deleteRequests = await moderatorRepository.GetArtistDeleteRequests();
            var requestsToReturn  = mapper.Map<List<ArtistForDeleteRequestDTO>>(deleteRequests);
            return Ok(requestsToReturn);
        }

        [HttpDelete("deleteRequest/{id}/{isApproved}")]
        public async Task<IActionResult> ReviewArtistDeleteRequest(int id, bool isApproved)
        {
            var deleteRequest = await moderatorRepository.GetArtistDeleteRequest(id);
            if (deleteRequest == null)
            {
                return BadRequest("The request does not exist");
            }
            if (isApproved)
            {
                if (await DeleteArtist(deleteRequest.ArtistId))
                {
                    return Ok();
                }
                return BadRequest("Unable to process request");
            }
            appRepository.Delete(deleteRequest);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to process request");
        }
        private async Task<bool> DeleteArtist(int id)
        {
            var artist = await moderatorRepository.GetArtistToReview(id);
                if (artist == null)
                {
                    return false;
                }
                var photos = new List<Photo>();
                foreach (var artistPhoto in artist.ArtistPhotos)
                {
                    photos.Add(artistPhoto.Photo);
                }
                foreach (var photo in photos)
                {
                    if (!(await DeletePhoto(photo)))
                    {
                        return false;
                    }
                }
                appRepository.Delete(artist);
                if (await appRepository.SaveAll())
                {
                    return true;
                }
                return false;
        }

        private async Task<bool> DeletePhoto(Photo photo)
        {
            var deletionParams = new DeletionParams(photo.PublicId);
            var result = cloudinary.Destroy(deletionParams);
            if (result.Result != "ok")
            {
                return false;
            }
            appRepository.Delete(photo);
            if (await appRepository.SaveAll())
            {
                return true;
            }
            return false;
        }
        
    }
}