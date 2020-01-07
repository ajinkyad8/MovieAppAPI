using MovieAppAPI.DTOs.Movie;
using MovieAppAPI.Models;

namespace MovieAppAPI.DTOs.Artist
{
    public class ArtistForMovieRoleDTO
    {
        public MovieForListDTO Movie { get; set; }
        public RoleType RoleType { get; set; }
        public string RoleDescription { get; set; }
        public bool IsApproved { get; set; }
        public int Id { get; set; }
    }
}