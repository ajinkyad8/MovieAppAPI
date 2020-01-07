using MovieAppAPI.DTOs.Auth;

namespace MovieAppAPI.DTOs.Artist
{
    public class ArtistForDeleteRequestDTO
    {
        public ArtistForListDTO Artist { get; set; }
        public int ArtistId { get; set; }
        public int Id { get; set; }
        public UserForReturnDTO User { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}