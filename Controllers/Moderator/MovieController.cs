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
using MovieAppAPI.DTOs.Movie;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers.Moderator
{
    [Authorize(Policy = "ModOnly")]
    [Route("api/moderator/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly Cloudinary cloudinary;
        private readonly IMapper mapper;
        private readonly IOptions<CloudinaryCredentials> cloudinaryCredentials;
        private readonly IModeratorRepository moderatorRepository;
        public MovieController(IModeratorRepository moderatorRepository, IAppRepository appRepository, IMapper mapper,
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
        public async Task<IActionResult> GetMoviesToReview()
        {
            var movies = await moderatorRepository.GetMoviesToReview();
            var moviesToReturn = mapper.Map<List<MovieForReviewDTO>>(movies);
            return Ok(moviesToReturn);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieToReview(int movieId)
        {
            var movie = await moderatorRepository.GetMovieToReview(movieId);
            return Ok(movie);
        }

        [HttpPost("review/{movieId}/{isApproved}")]
        public async Task<IActionResult> ReviewMovie(int movieId, bool isApproved)
        {
            var movieToReview = await moderatorRepository.GetMovieToReview(movieId);
            if (movieToReview.IsApproved)
            {
                return BadRequest("The movie has already been approved");
            }
            if (isApproved)
            {

                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                movieToReview.IsApproved = true;
                var firstActivity = movieToReview.ActivityLogs.First();
                firstActivity.ApprovedByModeratorId = modId;
                firstActivity.IsApproved = true;
                foreach (var movieRole in movieToReview.MovieRoles)
                {
                    movieRole.IsMovieApproved = true;
                }
                foreach (var activity in movieToReview.ActivityLogs)
                {
                    activity.IsMovieApproved = true;
                }
                foreach (var photo in movieToReview.MoviePhotos)
                {
                    photo.IsMovieApproved = true;
                }
                if (await appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Unable to approve movie");
            }
            else
            {                
                if (await DeleteMovie(movieToReview.Id))
                {
                    return Ok();
                }
                return BadRequest("Unable to reject movie");
            }
        }

        [HttpGet("activity")]
        public async Task<IActionResult> GetMovieActivityLogToReview()
        {
            var movieActivities = await moderatorRepository.GetMovieActivityLogToReview();
            var activityToRetrun = mapper.Map<List<MovieActivityToReturnDTO>>(movieActivities);
            return Ok(activityToRetrun);
        }

        [HttpPost("activity/{movieActivityId}/{isApproved}")]
        public async Task<IActionResult> ReviewMovieActivity(int movieActivityId, bool isApproved)
        {
            var movieActivity = await moderatorRepository.GetMovieActivityToReview(movieActivityId);
            if (isApproved)
            {
                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                movieActivity.ApprovedByModeratorId = modId;
                var movie = await appRepository.GetMovie(movieActivity.MovieId);
                movieActivity.IsApproved = true;
                var releaseDate = movie.ReleaseDate;
                mapper.Map(movieActivity, movie);
                if (movieActivity.ReleaseDate == null)
                {
                    movie.ReleaseDate = releaseDate;
                }
                if (await appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Unable to save changes");
            }
            appRepository.Delete(movieActivity);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to save changes");
        }
        
        [HttpGet("deleteRequests")]
        public async Task<IActionResult> GetMovieDeleteRequests()
        {
            var deleteRequests = await moderatorRepository.GetMovieDeleteRequests();
            var requestsToReturn = mapper.Map<List<MovieForDeleteRequestDTO>>(deleteRequests);
            return Ok(requestsToReturn);
        }

        [HttpDelete("deleteRequest/{id}/{isApproved}")]
        public async Task<IActionResult> ReviewMovieDeleteRequest(int id, bool isApproved)
        {
                var deleteRequest = await moderatorRepository.GetMovieDeleteRequest(id);
                if (deleteRequest == null)
                {
                    return BadRequest("The request does not exist");
                }
            if (isApproved)
            {
                if (await DeleteMovie(deleteRequest.MovieId))
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
        private async Task<bool> DeleteMovie(int id)
        {
            var movie = await moderatorRepository.GetMovieToReview(id);
            var photos = new List<Photo>();
                foreach (var moviePhoto in movie.MoviePhotos)
                {
                    photos.Add(moviePhoto.Photo);
                }
                foreach (var photo in photos)
                {
                    var isDeleted = await DeletePhoto(photo);
                    if (!isDeleted)
                    {
                        return false;
                    }
                }
            appRepository.Delete(movie);
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