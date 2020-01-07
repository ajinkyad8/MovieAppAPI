using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAppAPI.DTOs.Auth;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public AdminController(UserManager<User> userManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userManager.Users.Include("UserRoles.Role").ToListAsync();
            var usersToReturn = mapper.Map<List<UserForReturnDTO>>(users);
            return Ok(usersToReturn);
        }

        [HttpPost("{userName}")]
        public async Task<IActionResult> ChangeUserRoles(string userName, RolesToEditDTO rolesToEdit)
        {
            var user =await userManager.FindByNameAsync(userName);
            var userRoles =await userManager.GetRolesAsync(user);
            var roles = rolesToEdit.Roles;
            roles = roles ?? new string[] {};
            var result = await userManager.AddToRolesAsync(user, roles.Except(userRoles));
            if (!result.Succeeded)
            {
                return BadRequest("Unable to add roles");
            }
            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(roles));
            if (!result.Succeeded)
            {
                return BadRequest("Unable to remove roles");
            }
            return Ok(await userManager.GetRolesAsync(user));
        }
    }
}