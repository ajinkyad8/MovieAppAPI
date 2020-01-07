using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MovieAppAPI.Models
{
    public class User: IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }   
    }
}