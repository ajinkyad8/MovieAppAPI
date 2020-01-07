using System;

namespace MovieAppAPI.DTOs.Artist
{
    public class ArtistForUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool IsApproved { get; set; }
    }
}