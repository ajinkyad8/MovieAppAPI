using MovieAppAPI.DTOs.Photos;

namespace MovieAppAPI.DTOs.Movie
{
    public class MoviePhotoForReviewDTO
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int PhotoId { get; set; }
        public PhotoForReview Photo { get; set; }
        public bool IsMovieApproved { get; set; }
    }
}