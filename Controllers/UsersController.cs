using System.Collections.Generic;
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
using MovieAppAPI.DTOs.Movie;
using MovieAppAPI.DTOs.MovieRole;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly Cloudinary cloudinary;
        private readonly IMapper mapper;
        private readonly IAppRepository appRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper, IAppRepository appRepository,
        IOptions<CloudinaryCredentials> cloudinaryCredentials)
        {
            this.appRepository = appRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;

            cloudinary = new Cloudinary(new Account(cloudinaryCredentials.Value.CloudName, cloudinaryCredentials.Value.ApiKey,
                                        cloudinaryCredentials.Value.ApiSecret));

        }

        [HttpGet("{id}/artists")]
        public async Task<IActionResult> GetUserArtists(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var artists = await userRepository.GetUserArtists(id);
            var artistsToReturn = mapper.Map<List<ArtistForListDTO>>(artists);
            return Ok(artistsToReturn);
        }

        [HttpGet("{id}/artists/pending")]
        public async Task<IActionResult> GetUserPendingArtists(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var artists = await userRepository.GetUserPendingArtists(id);
            var artistsToReturn = mapper.Map<List<ArtistForListDTO>>(artists);
            return Ok(artistsToReturn);
        }

        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetUserMovies(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movies = await userRepository.GetUserMovies(id);
            var moviesToReturn = mapper.Map<List<MovieForListDTO>>(movies);
            return Ok(moviesToReturn);
        }

        [HttpGet("{id}/movies/pending")]
        public async Task<IActionResult> GetUserPendingMovies(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movies = await userRepository.GetUserPendingMovies(id);
            var moviesToReturn = mapper.Map<List<MovieForListDTO>>(movies);
            return Ok(moviesToReturn);
        }

        [HttpGet("{id}/movieRoles/")]
        public async Task<IActionResult> GetUserMovieRoles(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movieRoles = await userRepository.GetUserMovieRoles(id);
            var movieRolesToReturn = mapper.Map<List<MovieRoleForReviewDTO>>(movieRoles);
            return Ok(movieRolesToReturn);
        }

        [HttpGet("{id}/movieRoles/pending")]
        public async Task<IActionResult> GetUserPendingMovieRoles(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movieRoles = await userRepository.GetUserPendingMovieRoles(id);
            var movieRolesToReturn = mapper.Map<List<MovieRoleForReviewDTO>>(movieRoles);
            return Ok(movieRolesToReturn);
        }

        [HttpGet("{id}/artists/photos")]
        public async Task<IActionResult> GetUserArtistPhotos(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var artistPhotos = await userRepository.GetUserArtistPhotos(id);
            var artistPhotosToReturn = mapper.Map<List<ArtistPhotoForReviewDTO>>(artistPhotos);
            return Ok(artistPhotosToReturn);
        }

        [HttpGet("{id}/artists/photos/pending")]
        public async Task<IActionResult> GetUserPendingArtistPhotos(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var artistPhotos = await userRepository.GetUserPendingArtistPhotos(id);
            var artistPhotosToReturn = mapper.Map<List<ArtistPhotoForReviewDTO>>(artistPhotos);
            return Ok(artistPhotos);
        }

        [HttpGet("{id}/movies/photos")]
        public async Task<IActionResult> GetUserMoviePhotos(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var moviePhotos = await userRepository.GetUserMoviePhotos(id);
            var moviePhotosToReturn = mapper.Map<List<MoviePhotoForReviewDTO>>(moviePhotos);
            return Ok(moviePhotosToReturn);
        }

        [HttpGet("{id}/movies/photos/pending")]
        public async Task<IActionResult> GetUserPendingMoviePhotos(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var moviePhotos = await userRepository.GetUserPendingMoviePhotos(id);
            var moviePhotosToReturn = mapper.Map<List<MoviePhotoForReviewDTO>>(moviePhotos);
            return Ok(moviePhotos);
        }

        [HttpGet("{id}/artists/activity/")]
        public async Task<IActionResult> GetUserArtistActivityLog(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var artistActivity = await userRepository.GetUserArtistActivityLog(id);
            return Ok(artistActivity);
        }

        [HttpGet("{id}/artists/activity/pending")]
        public async Task<IActionResult> GetUserPendingArtistActivityLog(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var artistActivity = await userRepository.GetUserPendingArtistActivityLog(id);
            return Ok(artistActivity);
        }

        [HttpGet("{id}/movies/activity")]
        public async Task<IActionResult> GetUserMovieActivityLog(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movieActivity = await userRepository.GetUserMovieActivityLog(id);
            return Ok(movieActivity);
        }

        [HttpGet("{id}/movies/activity/pending")]
        public async Task<IActionResult> GetUserPendingMovGetUserPendingMovieActivityLogieRoles(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movieActivity = await userRepository.GetUserPendingMovieActivityLog(id);
            return Ok(movieActivity);
        }

        [HttpGet("{id}/movieRoles/activity/")]
        public async Task<IActionResult> GetUserMovieRoleActivityLog(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movieRoles = await userRepository.GetUserMovieRoleActivityLog(id);
            return Ok(movieRoles);
        }

        [HttpGet("{id}/movieRoles/activity/pending")]
        public async Task<IActionResult> GetUserPendingMovieRoleActivityLog(int id)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movieRoles = await userRepository.GetUserPendingMovieRoleActivityLog(id);
            return Ok(movieRoles);
        }

        [HttpDelete("{id}/artists/{artistId}")]
        public async Task<IActionResult> DeletePendingArtist(int id, int artistId)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var artist = await appRepository.GetArtist(artistId, id);
            if (artist == null)
            {
                return BadRequest("Either the artist does not exist or you do not have the permission to delete it");
            }
            if (artist.IsApproved)
            {
                return BadRequest("The artist is already approved. You need to submit a delete rquest for it from the artists page");
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
                    return BadRequest("The artist cannot be deleted at the moment");
                }
            }
            appRepository.Delete(artist);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to delete the artist");
        }

        [HttpDelete("{id}/movies/{movieId}")]
        public async Task<IActionResult> DeletePendingMovie(int id, int movieId)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movie = await appRepository.GetMovie(movieId, id);
            if (movie == null)
            {
                return BadRequest("Either the movie does not exist or you do not have the permission to delete it");
            }
            if (movie.IsApproved)
            {
                return BadRequest("The movie is already approved. You need to submit a delete request for it from the movies page");
            }
            var photos = new List<Photo>();
            foreach (var moviePhoto in movie.MoviePhotos)
            {
                photos.Add(moviePhoto.Photo);
            }
            foreach (var photo in photos)
            {
                if (!(await DeletePhoto(photo)))
                {
                    return BadRequest("The movie cannot be deleted at the moment");
                }
            }
            appRepository.Delete(movie);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to delete the movie");
        }

        [HttpDelete("{id}/movieRoles/{movieRoleId}")]
        public async Task<IActionResult> DeletePendingMovieRole(int id, int movieRoleId)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var movieRole = await appRepository.GetMovieRole(movieRoleId,id);
            if (movieRole == null)
            {
                return BadRequest("Either the role does not exist or you do not have the permission to delete it");
            }
            if (movieRole.IsApproved)
            {
                return BadRequest("The movie role is already approved. You need to submit a delete request for it from the movies page");
            }
            appRepository.Delete(movieRole);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to delete the movie role");
        }

        [HttpDelete("{id}/photos/{photoId}")]
        public async Task<IActionResult> DeletePendingPhoto(int id, int photoId)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var photo = await appRepository.GetPhoto(photoId, id);
            if (photo == null)
            {
                return BadRequest("Either the photo does not exist or you do not have the permission to delete it");
            }
            if (photo.IsApproved)
            {
                return BadRequest("The photo is already approved. You need to submit a delete request for it from the movie/artist page");
            }
            if (await DeletePhoto(photo))
            {
                return Ok();
            }
            return BadRequest("Unable to delete the photo");
        }

        private bool CheckUser(int id)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return false;
            }
            return true;
        }


        [HttpDelete("{id}/movieRoles/activity/{movieRoleActivityId}")]
        public async Task<IActionResult> DeletePendingMovieRoleActivity(int id, int movieRoleActivityId)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var activity = await userRepository.GetUserPendingMovieRoleActivity(id, movieRoleActivityId);
            if (activity == null)
            {
                return BadRequest("Either the edit does not exist or you do not have the permission to delete it");
            }
            if (activity.IsApproved)
            {
                return BadRequest("The movie role edit is already approved. You need to submit a delete request for it from the movies page");
            }
            appRepository.Delete(activity);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to delete the movie role edit");
        }
        
        [HttpDelete("{id}/movies/activity/{movieActivityId}")]
        public async Task<IActionResult> DeletePendingMovieActivity(int id, int movieActivityId)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var activity = await userRepository.GetUserPendingMovieActivity(id, movieActivityId);
            if (activity == null)
            {
                return BadRequest("Either the edit does not exist or you do not have the permission to delete it");
            }
            if (activity.IsApproved)
            {
                return BadRequest("The movie edit is already approved. You need to submit a delete request for it from the movies page");
            }
            appRepository.Delete(activity);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to delete the movie edit");
        }


        [HttpDelete("{id}/artists/activity/{artistActivityId}")]
        public async Task<IActionResult> DeletePendingArtistActivity(int id, int artistActivityId)
        {
            if (!CheckUser(id))
            {
                return Unauthorized();
            }
            var activity = await userRepository.GetUserPendingArtistActivity(id ,artistActivityId);
            if (activity == null)
            {
                return BadRequest("Either the edit does not exist or you do not have the permission to delete it");
            }
            if (activity.IsApproved)
            {
                return BadRequest("The artist edit is already approved. You need to submit a delete request for it from the artists page");
            }
            appRepository.Delete(activity);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to delete the artist edit");
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