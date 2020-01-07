using System;

namespace MovieAppAPI.Models
{
    public class MovieRoleActivityLog
    {        
        public int Id { get; set; }
        public MovieRole MovieRole { get; set; }
        public string RoleDescription { get; set; }
        public int MovieRoleId { get; set; }
        public bool IsMovieRoleApproved { get; set; }
        public bool IsApproved { get; set;}
        public User AddedByUser { get; set; }
        public int AddedByUserId { get; set; }
        public User ApprovedByModerator { get; set; }
        public int? ApprovedByModeratorId { get; set; }
        public DateTime TimeStamp { get; set; }
        public MovieRoleActivityLog()
        {
            TimeStamp = DateTime.Now;
        }
    }
}