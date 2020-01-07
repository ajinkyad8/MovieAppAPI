using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Data;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [Authorize(Policy="UserOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRolesController : ControllerBase
    {

        private readonly IAppRepository appRepository;
        public MovieRolesController(IAppRepository appRepository)
        {
            this.appRepository = appRepository;

        }

        [HttpGet("{id}", Name = "GetMovieRole")]
        public async Task<IActionResult> GetMovieRole(int id)
        {
            var movieRole = await appRepository.GetMovieRole(id);
            if (movieRole == null)
            {
                return BadRequest("The movie role does not exist");
            }
            return Ok(movieRole);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieRoles()
        {
            var movieRoles = await appRepository.GetMovieRoles();
            return Ok(movieRoles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieRole(MovieRole movieRole)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (await appRepository.GetMovieRoleByParams(movieRole.MovieId,movieRole.ArtistId,movieRole.RoleTypeId) != null || 
            await appRepository.GetMovieRoleByParams(movieRole.MovieId, movieRole.ArtistId, movieRole.RoleTypeId, userId) != null)
            {
                return BadRequest("The movie role already exists");
            }
            var artist = await appRepository.GetArtist(movieRole.ArtistId);
            if (artist == null)
            {
                artist = await appRepository.GetArtist(movieRole.ArtistId, userId);
            }
            if (artist == null)
            {
                return BadRequest("The artist does not exist");
            }
            movieRole.Artist = artist;
            movieRole.IsArtistApproved = artist.IsApproved;
            var movie = await appRepository.GetMovie(movieRole.MovieId);
            if (movie == null)
            {
                movie = await appRepository.GetMovie(movieRole.MovieId, userId);
            }
            if (movie == null)
            {
                return BadRequest("The movie does not exist");

            }
            movieRole.Movie = movie;
            movieRole.IsMovieApproved = movie.IsApproved;
            var roleType = await appRepository.GetRoleType(movieRole.RoleTypeId);
            if (roleType == null)
            {
                return BadRequest("The role type does not exist");
            }
            appRepository.Add(movieRole);
            if (await appRepository.SaveAll())
            {
                var activity = new MovieRoleActivityLog();
                activity.RoleDescription = movieRole.RoleDescription;
                activity.AddedByUserId = userId;
                activity.MovieRoleId = movieRole.Id;
                appRepository.Add(activity);
                if (await appRepository.SaveAll())
                {
                    return CreatedAtRoute("GetMovieRole", new { id = movieRole.Id}, movieRole);
                }
                return BadRequest("Unable to Add Movie Role");
            }
            return BadRequest("Unable to Add Movie Role");
        }

        [HttpPost("deleteRequest/{id}")]
        public async Task<IActionResult> DeleteMovieRole(int id, MovieRoleDeleteRequest deleteRequest)
        {
            var movieRole = await appRepository.GetMovieRole(id);
            if (movieRole == null)
            {
                return BadRequest("The movie role does not exist");
            }
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != deleteRequest.UserId)
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
        public async Task<IActionResult> UpdateMovieRole(MovieRoleActivityLog movieRoleActivity)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var movieRole = await appRepository.GetMovieRole(movieRoleActivity.MovieRoleId);
            if (movieRole == null)
            {
                movieRole = await appRepository.GetMovieRole(movieRoleActivity.MovieRoleId, userId);
            }
            if (movieRole == null)
            {
                return BadRequest("The movie role does not exist");
            }
            if (!movieRole.IsApproved)
            {
                var firstActivity = await appRepository.GetMovieRoleActivity(movieRole.ActivityLogs.FirstOrDefault().Id);
                movieRole.RoleDescription = movieRoleActivity.RoleDescription;
                firstActivity.RoleDescription = movieRoleActivity.RoleDescription;
            if (await appRepository.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Unable to update movie role");
            }
            movieRoleActivity.IsMovieRoleApproved = true;
            movieRoleActivity.AddedByUserId = userId;
            appRepository.Add(movieRoleActivity);
            if (await appRepository.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Unable to update movie role");
        }
    }
}