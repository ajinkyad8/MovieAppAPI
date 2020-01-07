using Microsoft.AspNetCore.Http;

namespace MovieAppAPI.DTOs.Photos
{
    public class PhotoForUploadDTO
    {
        public IFormFile File { get; set; }
    }
}