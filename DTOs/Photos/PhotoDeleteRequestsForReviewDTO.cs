using MovieAppAPI.DTOs.Auth;

namespace MovieAppAPI.DTOs.Photos
{
    public class PhotoDeleteRequestsForReviewDTO
    {
        public int Id { get; set; }
        public UserForReturnDTO User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
    }
}