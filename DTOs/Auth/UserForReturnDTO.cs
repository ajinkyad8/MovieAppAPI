using System.Collections.Generic;

namespace MovieAppAPI.DTOs.Auth
{
    public class UserForReturnDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<UserRoleToReturnDTO> UserRoles { get; set; }
    }
}