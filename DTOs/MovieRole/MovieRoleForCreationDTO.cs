namespace MovieAppAPI.DTOs.MovieRole
{
    public class MovieRoleForCreationDTO
    {
        public int MovieId { get; set; }
        public int ArtistId { get; set; }
        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
    }
}