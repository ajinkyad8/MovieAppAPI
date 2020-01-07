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
using MovieAppAPI.DTOs.Photos;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers.Moderator
{
    [Authorize(Policy = "ModOnly")]
    [Route("api/moderator/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly Cloudinary cloudinary;
        private readonly IMapper mapper;
        private readonly IOptions<CloudinaryCredentials> cloudinaryCredentials;
        private readonly IModeratorRepository moderatorRepository;
        public PhotoController(IModeratorRepository moderatorRepository, IAppRepository appRepository, IMapper mapper,
        IOptions<CloudinaryCredentials> cloudinaryCredentials)
        {
            this.moderatorRepository = moderatorRepository;
            this.cloudinaryCredentials = cloudinaryCredentials;
            this.mapper = mapper;
            this.appRepository = appRepository;

            cloudinary = new Cloudinary(new Account(cloudinaryCredentials.Value.CloudName, cloudinaryCredentials.Value.ApiKey,
                                        cloudinaryCredentials.Value.ApiSecret));

        }


        [HttpGet("artists")]
        public async Task<IActionResult> GetArtistPhotosToReview()
        {
            var artistPhotos = await moderatorRepository.GetArtistPhotosToReview();
            var artistPhotosToReturn = mapper.Map<List<ArtistPhotoForReviewDTO>>(artistPhotos);
            return Ok(artistPhotosToReturn);
        }

        [HttpPost("artist/{artistId}/{photoId}/{isApproved}")]
        public async Task<IActionResult> ReviewArtistPhoto(int artistId, int photoId, bool isApproved)
        {
            var artistPhoto = await appRepository.GetArtistPhoto(artistId, photoId);
            if (isApproved)
            {
                artistPhoto.Photo.IsApproved = true;
                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                artistPhoto.Photo.ApprovedByModeratorId = modId;
                if (await appRepository.GetArtistDisplayPhoto(artistId) == null)
                {
                    artistPhoto.Photo.IsMain = true;
                }
            }
            else
            {
                var deleted = await DeletePhoto(artistPhoto.Photo);
                if (!deleted)
                {
                    return BadRequest("Unable to reject photo");
                }
                return Ok();
            }
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to accept photo");
        }

        [HttpGet("artists/deleteRequests")]
        public async Task<IActionResult> GetArtistPhotosWithDeleteRequests()
        {
            var deleteRequests = await moderatorRepository.GetArtistPhotosWithDeleteRequests();
            return Ok(deleteRequests);
        }

        [HttpGet("artist/deleteRequests/{photoId}")]
        public async Task<IActionResult> GetArtistPhotoDeleteRequests(int photoId)
        {
            var deleteRequests = await moderatorRepository.GetPhotoDeleteRequests(photoId);
            var requestsToReturn = mapper.Map<List<PhotoDeleteRequestsForReviewDTO>>(deleteRequests);
            return Ok(requestsToReturn);
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetMoviePhotosToReview()
        {
            var moviePhotos = await moderatorRepository.GetMoviePhotosToReview();
            var moviePhotosToReturn = mapper.Map<List<MoviePhotoForReviewDTO>>(moviePhotos);
            return Ok(moviePhotosToReturn);
        }

        [HttpPost("movie/{movieId}/{photoId}/{isApproved}")]
        public async Task<IActionResult> ReviewMoviePhoto(int movieId, int photoId, bool isApproved)
        {
            var moviePhoto = await appRepository.GetMoviePhoto(movieId, photoId);
            if (isApproved)
            {
                moviePhoto.Photo.IsApproved = true;
                var modId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                moviePhoto.Photo.ApprovedByModeratorId = modId;
                if (await appRepository.GetMovieDisplayPhoto(movieId) == null)
                {
                    moviePhoto.Photo.IsMain = true;
                }
            }
            else
            {
                var deleted = await DeletePhoto(moviePhoto.Photo);
                if (!deleted)
                {
                    return BadRequest("Unable to reject photo");
                }
                return Ok();
            }
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to accept photo");
        }



        [HttpGet("movies/deleteRequests")]
        public async Task<IActionResult> GetMoviePhotosWithDeleteRequests()
        {
            var deleteRequests = await moderatorRepository.GetMoviePhotosWithDeleteRequests();
            return Ok(deleteRequests);
        }

        [HttpGet("movie/deleteRequests/{photoId}")]
        public async Task<IActionResult> GetMoviePhotoDeleteRequests(int photoId)
        {
            var deleteRequests = await moderatorRepository.GetPhotoDeleteRequests(photoId);
            var requestsToReturn = mapper.Map<List<PhotoDeleteRequestsForReviewDTO>>(deleteRequests);
            return Ok(requestsToReturn);
        }

        [HttpDelete("deleteRequest/{id}/{isApproved}")]
        public async Task<IActionResult> ReviewMoviePhotoDeleteRequest(int id, bool isApproved)
        {
            var deleteRequest = await moderatorRepository.GetPhotoDeleteRequest(id);
            if (deleteRequest == null)
            {
                return BadRequest("The request does not exist");
            }
            if (isApproved)
            {
                var photo = await appRepository.GetPhoto(deleteRequest.PhotoId);
                if (photo.IsMain)
                {
                    return BadRequest("Cannot delete main photo. Change the main photo before deleting this.");
                }
                if (await DeletePhoto(photo))
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