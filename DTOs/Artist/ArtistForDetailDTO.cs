using System;
using System.Collections.Generic;

namespace MovieAppAPI.DTOs.Artist
{
    public class ArtistForDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsApproved { get; set; }
        public ICollection<ArtistForPhotoDTO> ArtistPhotos { get; set; }
        public ICollection<ArtistForMovieRoleDTO> MovieRoles { get; set; }
        
    }
}