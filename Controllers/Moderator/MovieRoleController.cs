using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieAppAPI.Data;
using MovieAppAPI.DTOs.MovieRole;
using MovieAppAPI.Helpers;

namespace MovieAppAPI.Controllers.Moderator
{
    [Authorize(Policy = "ModOnly")]
    [Route("api/moderator/[controller]")]
    [ApiController]
    public class MovieRoleController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly Cloudinary cloudinary;
        private readonly IMapper mapper;
        private readonly IOptions<CloudinaryCredentials> cloudinaryCredentials;
        private readonly IModeratorRepository moderatorRepository;
        public MovieRoleController(IModeratorRepository moderatorRepository, IAppRepository appRepository, IMapper mapper,
        IOptions<CloudinaryCredentials> cloudinaryCredentials)
        {
            this.moderatorRepository = moderatorRepository;
            this.cloudinaryCredentials = cloudinaryCredentials;
            this.mapper = mapper;
            this.appRepository = appRepository;

            cloudinary = new Cloudinary(new Account(cloudinaryCredentials.Value.CloudName, cloudinaryCredentials.Value.ApiKey,
                                        cloudinaryCredentials.Value.ApiSecret));

        }
        [HttpGet]
        public async Task<IActionResult> GetMovieRolesToReview()
        {
            var movieRolesToReview = await moderatorRepository.GetMovieRolesToReview();
            var moviesRolesToReturn = mapper.Map<List<MovieRoleForReviewDTO>>(movieRolesToReview);
            return Ok(moviesRolesToReturn);
        }

        [HttpPost("{id}/{isApproved}")]
        public async Task<IActionResult> ReviewMovieRole(int id, bool isApproved)
        {
            var movieRole = await moderatorRepository.GetMovieRoleToReview(id);
            if (movieRole == null)
            {
                return BadRequest("The movie role does not exist");
            }
            if (!movieRole.IsArtistApproved)
            {
                return BadRequest("You need to review the artist before you can review this role");
            }
            if (!movieRole.IsMovieApproved)
            {
                return BadRequest("You need to review the movie before you can review this role");
            }
            if (isApproved)
            {
                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                movieRole.IsApproved = true;
                var firstActivity = movieRole.ActivityLogs.FirstOrDefault();
                firstActivity.IsApproved = true;
                firstActivity.ApprovedByModeratorId = modId;
                foreach (var activity in movieRole.ActivityLogs)
                {
                    activity.IsMovieRoleApproved = true;
                }
            }
            else
            {
                appRepository.Delete(movieRole);
            }
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to review this movie role");
        }

        [HttpGet("activity")]
        public async Task<IActionResult> GetActivityToReview()
        {
            var activities = await moderatorRepository.GetMovieRoleActivityLogToReview();
            var activityToReturn = mapper.Map<List<MovieRoleActivityToReturn>>(activities);
            return Ok(activityToReturn);
        }

        [HttpPost("activity/{id}/{isApproved}")]
        public async Task<IActionResult> ReviewMovieRoleActivity(int id, bool isApproved)
        {
            var activity = await moderatorRepository.GetMovieRoleActivityToReview(id);
            if (isApproved)
            {
                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                activity.IsApproved = true;
                activity.ApprovedByModeratorId = modId;
                var movieRole =await appRepository.GetMovieRole(activity.MovieRoleId);
                movieRole.RoleDescription = activity.RoleDescription;
                if (await appRepository.SaveAll())
                {
                    return Ok();
                }
                return BadRequest("Unable to review activity");
            }
            appRepository.Delete(activity);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to review activity");
        }    
        
        [HttpGet("deleteRequests")]
        public async Task<IActionResult> GetMovieRoleDeleteRequests()
        {
            var deleteRequests = await moderatorRepository.GetMovieRoleDeleteRequests();
            var requestsToReturn = mapper.Map<List<MovieRoleForDeleteRequestDTO>>(deleteRequests);
            return Ok(requestsToReturn);
        }

        [HttpDelete("deleteRequest/{id}/{isApproved}")]
        public async Task<IActionResult> ReviewMovieRoleDeleteRequest(int id, bool isApproved)
        {
            var deleteRequest = await moderatorRepository.GetMovieRoleDeleteRequest(id);
            if (isApproved)
            {
                var movieRole = await moderatorRepository.GetMovieRoleToReview(deleteRequest.MovieRoleId);
                appRepository.Delete(movieRole);
            }
            else
            {
                appRepository.Delete(deleteRequest);
            }
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to review request");
        }
    }
}