using System;

namespace MovieAppAPI.Models
{
    public class ArtistActivityLog
    {
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool IsApproved { get; set; }
        public bool IsArtistApproved { get; set; }
        public User AddedByUser { get; set; }
        public int AddedByUserId { get; set; }
        public User ApprovedByModerator { get; set; }
        public int? ApprovedByModeratorId { get; set; }
        public DateTime TimeStamp { get; set; }
        public ArtistActivityLog()
        {
            TimeStamp = DateTime.Now;
        }
        
    }
}