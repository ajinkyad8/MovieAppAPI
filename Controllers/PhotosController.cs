using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieAppAPI.Data;
using MovieAppAPI.DTOs.Photos;
using MovieAppAPI.Helpers;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly Cloudinary cloudinary;
        private readonly IOptions<CloudinaryCredentials> cloudinaryCredentials;
        public PhotosController(IAppRepository appRepository, IOptions<CloudinaryCredentials> cloudinaryCredentials)
        {
            this.cloudinaryCredentials = cloudinaryCredentials;
            this.appRepository = appRepository;

            cloudinary = new Cloudinary(new Account(cloudinaryCredentials.Value.CloudName , cloudinaryCredentials.Value.ApiKey,
                                        cloudinaryCredentials.Value.ApiSecret));
        }


        [HttpGet("artist/{artistId}")]
        public Task<IActionResult> GetArtistPhotos(int artistId)
        {
            return null;
        }

        [HttpGet("artist/{artistId}/{photoId}", Name="GetArtistPhoto")]
        public async Task<IActionResult> GetArtistPhoto(int artistId, int photoId)
        {
            var photo = await appRepository.GetArtistPhoto(artistId, photoId);
            if (photo == null)
            {
                return BadRequest("No photo with id " + photoId + " and artistId " + artistId);
            }
            return Ok(photo);
        }

        [HttpPost("artist/{artistId}")]
        public async Task<IActionResult> AddArtistPhoto(int artistId, [FromForm]PhotoForUploadDTO photoForUploadDTO)
        {
            var artist = await appRepository.GetArtist(artistId);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (artist == null)
            {
                artist = await appRepository.GetArtist(artistId, userId);
            }
            if (artist == null)
            {
                return BadRequest("Artist does not exist");
            }
            if (photoForUploadDTO.File.Length > 10*1024*1024)
            {
                return BadRequest("Max file size should be 10 MB.");
            }
            using (var stream = photoForUploadDTO.File.OpenReadStream())
            {
                if (!IsImage(stream))
                {
                    return BadRequest("The file " + photoForUploadDTO.File.FileName + " is not an image!");
                }
            }
            var photo = await UploadPhoto(photoForUploadDTO.File);
            if (photo == null)
            {
                return BadRequest("Unable to upload photo");
            }
            var artistPhoto = new ArtistPhoto();
            if (artist.IsApproved)
            {
                artistPhoto.IsArtistApproved = true;
            }
            artistPhoto.Artist = artist;
            artistPhoto.ArtistId = artist.Id;
            artistPhoto.Photo = photo;
            artistPhoto.PhotoId = photo.Id;
            appRepository.Add(artistPhoto);
            if (await appRepository.SaveAll())
            {
                return CreatedAtRoute("GetArtistPhoto", new{artistId = artist.Id, photoId = photo.Id}, artistPhoto);
            }
            return BadRequest("Unable to add photo");
        }

        [HttpPost("artist/deleteRequest/{artistId}/{photoId}")]
        public async Task<IActionResult> DeleteArtistPhoto(int artistId, int photoId, PhotoDeleteRequest deleteRequest)
        {
            var artistPhoto = await appRepository.GetArtistPhoto(artistId,photoId);
            if (artistPhoto == null)
            {
                return BadRequest("The photo does not exist.");
            }
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != deleteRequest.UserId)
            {
                return Unauthorized();
            }
            appRepository.Add(deleteRequest);
            if (await appRepository.SaveAll())
            {                
                return NoContent();
            }
            
            return BadRequest("Unable to delete photo.");
        }

        [HttpPut("artist/{artistId}/{photoId}")]
        public async Task<IActionResult> SetArtistDisplayPicture(int artistId, int photoId)
        {
            var newDisplayPhoto = await appRepository.GetArtistPhoto(artistId, photoId);
            if (newDisplayPhoto == null)
            {
                return BadRequest("The photo does not exist");
            }
            if (newDisplayPhoto.Photo.IsMain)
            {
                return BadRequest("The photo is already the display picture.");
            }
            var currentDisplayPhoto = await appRepository.GetArtistDisplayPhoto(artistId);
            if (currentDisplayPhoto != null)
            {
                currentDisplayPhoto.Photo.IsMain = false;
            }
            newDisplayPhoto.Photo.IsMain = true;
            if (await appRepository.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Unable to set the display picture");
        }

        [HttpGet("movie/{movieId}")]
        public Task<IActionResult> GetMoviePhotos(int movieId)
        {
            return null;
        }

        [HttpGet("movie/{movieId}/{photoId}", Name="GetMoviePhoto")]
        public async Task<IActionResult> GetMoviePhoto(int movieId, int photoId)
        {
            var photo = await appRepository.GetMoviePhoto(movieId, photoId);
            if (photo == null)
            {
                return BadRequest("The photo does not exist");
            }
            return Ok(photo);
        }

        [HttpPost("movie/{movieId}")]
        public async Task<IActionResult> AddMoviePhoto(int movieId, [FromForm]PhotoForUploadDTO photoForUploadDTO)
        {
            var movie = await appRepository.GetMovie(movieId);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (movie == null)
            {
                movie = await appRepository.GetMovie(movieId, userId);
            }
            if (movie == null)
            {
                return BadRequest("Movie does not exist");
            }
            if (photoForUploadDTO.File.Length > 10*1024*1024)
            {
                return BadRequest("Max file size should be 10 MB.");
            }
            using (var stream = photoForUploadDTO.File.OpenReadStream())
            {
                if (!IsImage(stream))
                {
                    return BadRequest("The file " + photoForUploadDTO.File.FileName + " is not an image!");
                }
            }
            var photo = await UploadPhoto(photoForUploadDTO.File);
            if (photo == null)
            {
                return BadRequest("Unable to upload photo");
            }
            var moviePhoto = new MoviePhoto();
            if (movie.IsApproved)
            {
                moviePhoto.IsMovieApproved = true;
            }
            moviePhoto.Movie = movie;
            moviePhoto.MovieId = movie.Id;
            moviePhoto.Photo = photo;
            moviePhoto.PhotoId = photo.Id;
            appRepository.Add(moviePhoto);
            if (await appRepository.SaveAll())
            {
                return CreatedAtRoute("GetMoviePhoto", new{movieId = movie.Id, photoId = photo.Id}, moviePhoto);
            }
            return BadRequest("Unable to add photo");
        }

        [HttpPost("movie/deleteRequest/{movieId}/{photoId}")]
        public async Task<IActionResult> DeleteMoviePhoto(int movieId, int photoId, PhotoDeleteRequest deleteRequest)
        {
            var moviePhoto = await appRepository.GetMoviePhoto(movieId,photoId);
            if (moviePhoto == null)
            {
                return BadRequest("The photo does not exist.");
            }
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != deleteRequest.UserId)
            {
                return Unauthorized();
            }
            appRepository.Add(deleteRequest);
            if (await appRepository.SaveAll())
            {                
                return NoContent();
            }
            
            return BadRequest("Unable to delete photo.");
        }

        [HttpPut("movie/{movieId}/{photoId}")]
        public async Task<IActionResult> SetMovieDisplayPicture(int movieId, int photoId)
        {
            var newDisplayPhoto = await appRepository.GetMoviePhoto(movieId, photoId);
            if (newDisplayPhoto == null)
            {
                return BadRequest("The photo does not exist");
            }
            if (newDisplayPhoto.Photo.IsMain)
            {                
                return BadRequest("The photo is already the display picture.");
            }
            var currentDisplayPhoto = await appRepository.GetMovieDisplayPhoto(movieId);
            if (currentDisplayPhoto != null)
            {
                currentDisplayPhoto.Photo.IsMain = false;
            }
            newDisplayPhoto.Photo.IsMain = true;
            if (await appRepository.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Unable to set the display picture");
        }

        private async Task<Photo> UploadPhoto(IFormFile file)
        {
            var imageUploadResult = new ImageUploadResult();
            if (file.Length>0)
            {
                using (var fileStream = file.OpenReadStream())
                {
                    var imageUploadParams = new ImageUploadParams{
                        File = new FileDescription(file.Name, fileStream),
                        Transformation = new Transformation().Height(500).Width(500).Background("black").Crop("pad")
                    };
                    imageUploadResult = cloudinary.Upload(imageUploadParams);
                }
            }
            if (imageUploadResult.PublicId == null)
            {
                return null;
            }

            var photo = new Photo();
            photo.PublicId = imageUploadResult.PublicId;
            photo.URL = imageUploadResult.Uri.ToString();
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            photo.AddedByUserId = userId;
            appRepository.Add(photo);
            if (await appRepository.SaveAll())
            {
                return photo;
            }
            return null;
        }

        public bool IsImage(Stream stream)
        {            
            var ImageTypes = new Dictionary<string, string>();
            ImageTypes.Add("FFD8","jpg");
            ImageTypes.Add("424D","bmp");
            ImageTypes.Add("474946","gif");
            ImageTypes.Add("89504E470D0A1A0A","png");    
            StringBuilder builder = new StringBuilder();
            int largestByteHeader = ImageTypes.Max(img => img.Key.Length);
    
            for (int i = 0; i < largestByteHeader/2; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                builder.Append(bit);
    
                string builtHex = builder.ToString();
                bool isImage = ImageTypes.Keys.Any(img => img == builtHex);
                if (isImage)
                {
                    return true;
                }
            }
        return false;
        } 

    }
}