using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieAppAPI.DTOs.Auth;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration,
        IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            var user = _mapper.Map<User>(userForRegisterDTO);
            var result = await _userManager.CreateAsync(user, userForRegisterDTO.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return StatusCode(201);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDTO.UserName);
            if (user == null)
            {
                return BadRequest("There exists no user with the given user name.");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDTO.Password, false);
            if (result.Succeeded)
            {
                var userFromRepo = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == user.NormalizedUserName);
                return Ok
                (new
                {
                    token =await GenerateJWTToken(userFromRepo)
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        private async Task<string> GenerateJWTToken(User userFromRepo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };
            var roles = await _userManager.GetRolesAsync(userFromRepo);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));   
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}