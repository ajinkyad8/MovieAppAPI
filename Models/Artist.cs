using System;
using System.Collections.Generic;

namespace MovieAppAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool IsApproved { get; set; }
        public ICollection<MovieRole> MovieRoles { get; set; }
        public ICollection<ArtistPhoto> ArtistPhotos { get; set; }
        public ICollection<ArtistActivityLog> ActivityLogs { get; set; }
        public ICollection<ArtistDeleteRequest> DeleteRequests { get; set; }
    }
}