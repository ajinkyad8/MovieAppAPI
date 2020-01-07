using MovieAppAPI.DTOs.Auth;

namespace MovieAppAPI.DTOs.Movie
{
    public class MovieForDeleteRequestDTO
    {        
        public MovieForListDTO Movie { get; set; }
        public int MovieId { get; set; }
        public int Id { get; set; }
        public UserForReturnDTO User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}