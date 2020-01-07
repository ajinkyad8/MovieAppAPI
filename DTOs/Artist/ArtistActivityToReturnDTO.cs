using System;

namespace MovieAppAPI.DTOs.Artist
{
    public class ArtistActivityToReturnDTO
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }

    }
}