using System;

namespace MovieAppAPI.DTOs.Auth
{
    public class UserForRegisterDTO
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}