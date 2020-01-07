using System.Collections.Generic;

namespace MovieAppAPI.Models
{
    public class MovieRole
    {
        public int Id { get; set; }
        public Artist Artist { get; set; }
        public int ArtistId { get; set;}
        public RoleType RoleType { get; set; }
        public int RoleTypeId { get; set;}
        public Movie Movie { get; set; }
        public int MovieId { get; set;}
        public string RoleDescription { get; set; }
        public bool IsArtistApproved { get; set; }
        public bool IsMovieApproved { get; set; }
        public bool IsApproved { get; set;}
        public ICollection<MovieRoleActivityLog> ActivityLogs { get; set; }
        public ICollection<MovieRoleDeleteRequest> DeleteRequests { get; set; }
    }
}