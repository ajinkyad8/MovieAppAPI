namespace MovieAppAPI.DTOs.MovieRole
{
    public class MovieRoleForReviewDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ArtistId { get; set; }
        public int RoleTypeId { get; set; }
        public string RoleDescription { get; set; }
        public string MovieName { get; set; }
        public string ArtistName { get; set; }
        public string RoleName { get; set; }
        public bool IsApproved { get; set; }
        public string ArtistPhotoUrl { get; set; }
        public string UserName { get; set; }
    }
}