using MovieAppAPI.DTOs.Auth;

namespace MovieAppAPI.DTOs.MovieRole
{
    public class MovieRoleForDeleteRequestDTO
    {    
        public MovieRoleForReviewDTO MovieRole { get; set; }
        public int MovieRoleId { get; set; }
        public int Id { get; set; }
        public UserForReturnDTO User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}