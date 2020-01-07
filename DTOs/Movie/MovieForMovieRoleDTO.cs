using MovieAppAPI.DTOs.Artist;
using MovieAppAPI.Models;

namespace MovieAppAPI.DTOs.Movie
{
    public class MovieForMovieRoleDTO
    {
        public ArtistForListDTO Artist { get; set; }
        public RoleType RoleType { get; set; }
        public string RoleDescription { get; set; }
        public bool IsApproved { get; set; }
        public int Id { get; set; }
        
    }
}