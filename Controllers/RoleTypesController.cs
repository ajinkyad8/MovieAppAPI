using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Data;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleTypesController : ControllerBase
    {
        
        private readonly IAppRepository appRepository;
        public RoleTypesController(IAppRepository appRepository)
        {
            this.appRepository = appRepository;

        }

        [HttpGet("{id}", Name = "GetRoleType")]
        public async Task<IActionResult> GetRoleType(int id)
        {
            var roleType = await appRepository.GetRoleType(id);
            if (roleType == null)
            {
                return BadRequest("The role type does not exist");
            }
            return Ok(roleType);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRoleTypes()
        {
            var roleTypes = await appRepository.GetRoleTypes();
            return Ok(roleTypes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleType(RoleType roleType)
        {
            appRepository.Add(roleType);
            if (await appRepository.SaveAll())
            {
                return CreatedAtRoute("GetRoleType", new { id = roleType.Id }, roleType);
            }
            return BadRequest("Unable to Add Role Type");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleType(int id)
        {
            var roleType = await appRepository.GetRoleType(id);
            if (roleType == null)
            {
                return BadRequest("The role type does not exist");
            }
            appRepository.Delete(roleType);
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to Delete Role Type");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRoleType(int id, RoleType role)
        {
            var roleTypeFromRepo = await appRepository.GetRoleType(id);
            if (roleTypeFromRepo == null)
            {
                return BadRequest("The role type does not exist");
            }
            roleTypeFromRepo.RoleName = role.RoleName;
            if (await appRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Unable to edit role type");
        }
    }
}