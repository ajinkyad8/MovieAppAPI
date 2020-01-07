using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Data;
using MovieAppAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MovieAppAPI.Helpers;
using System;
using MovieAppAPI.DTOs.Movie;

namespace MovieAppAPI.Controllers
{
    [Authorize(Policy="UserOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly IMapper mapper;

        public MoviesController(IAppRepository appRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.appRepository = appRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery]QueryParams queryParams)
        {
            var movies = new PaginatedList<Movie>();
            if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                movies = await appRepository.GetMovies(userId, queryParams);
            }
            else
            {
                movies = await appRepository.GetMovies(queryParams);
            }
            Response.AddPaginationHeader(movies.PageNumber, movies.PageSize, movies.TotalPages, movies.TotalItems);
            var moviesToReturn = mapper.Map<IEnumerable<MovieForListDTO>>(movies);
            return Ok(moviesToReturn);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await appRepository.GetMovie(id);
            if (movie !=null && User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userMovieRoles = await appRepository.GetUserMovieMovieRoles(id, userId);
                movie.MovieRoles.Concat(userMovieRoles).ToList();
                var userMoviePhotos = await appRepository.GetUserMoviePhotos(id, userId);
                movie.MoviePhotos.Concat(userMoviePhotos).ToList();
            }
            if (movie == null && User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                movie = await appRepository.GetMovie(id, userId);
            }
            if (movie == null)
            {
                return BadRequest("The movie does not exist");
            }
            var movieToReturn = mapper.Map<MovieForDetailDTO>(movie);
            return Ok(movieToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            appRepository.Add(movie);
            if (await appRepository.SaveAll())
            {
                var activity = mapper.Map<MovieActivityLog>(movie);
                activity.MovieId = movie.Id;
                activity.AddedByUserId = userId;
                appRepository.Add(activity);
                if (await appRepository.SaveAll())
                {
                    return CreatedAtRoute("GetMovie", new { id = movie.Id }, movie);
                }
                return BadRequest("Unable to Add Movie");
            }
            return BadRequest("Unable to Add Movie");
        }

        [HttpPost("deleteRequest/{id}")]
        public async Task<IActionResult> DeleteMovie(int id, MovieDeleteRequest deleteRequest)
        {
            var movie = await appRepository.GetMovie(id);
            if (movie == null)
            {
                return BadRequest("The movie does not exist");
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
            return BadRequest("Unable to Delete Movie");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(MovieActivityLog movieActivityLog)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var movieFromRepo = await appRepository.GetMovie(movieActivityLog.MovieId);
            if (movieFromRepo == null)
            {
                movieFromRepo = await appRepository.GetMovie(movieActivityLog.MovieId, userId);
            }
            if (movieFromRepo == null)
            {
                return BadRequest("The movie does not exist");
            }
            if (!movieFromRepo.IsApproved)
            {
                var firstActivity = await appRepository.GetMovieActivity(movieFromRepo.ActivityLogs.FirstOrDefault().Id);
                mapper.Map(movieActivityLog, movieFromRepo);
                mapper.Map(movieFromRepo, firstActivity);
                if (await appRepository.SaveAll())
                {
                    var movieToReturn = mapper.Map<MovieForDetailDTO>(movieFromRepo);
                    return Ok(movieToReturn);
                }
                return BadRequest("Unable to update Movie");
            }
            movieActivityLog.IsMovieApproved = true;
            movieActivityLog.AddedByUserId = userId;
            appRepository.Add(movieActivityLog);
            if (await appRepository.SaveAll())
            {
                var movieToReturn = mapper.Map<MovieForDetailDTO>(movieFromRepo);
                return Ok(movieToReturn);
            }
            return BadRequest("Unable to update Movie");
        }

        [AllowAnonymous]
        [HttpGet("{id}/activity")]
        public async Task<IActionResult> GetMovieActivity(int id)
        {
            var activityLog = await appRepository.GetMovieActivityLog(id);
            var activityLogToReturn = mapper.Map<List<MovieActivityToReturnDTO>>(activityLog);
            return Ok(activityLogToReturn);
        }
    }
}