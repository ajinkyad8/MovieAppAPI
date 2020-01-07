using MovieAppAPI.DTOs.Photos;

namespace MovieAppAPI.DTOs.Artist
{
    public class ArtistPhotoForReviewDTO
    {        
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public int PhotoId { get; set; }
        public PhotoForReview Photo { get; set; }
        public bool IsArtistApproved { get; set; }
    }
}