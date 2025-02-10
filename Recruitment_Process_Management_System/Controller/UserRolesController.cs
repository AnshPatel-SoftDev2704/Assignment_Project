using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoles>>> getAllUserRole()
        {
            try{
                var userRoles = await _userRolesService.getAllUserRoles();
                return Ok(userRoles);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{UserRolesId}")]
        public async Task<ActionResult<IEnumerable<UserRoles>>> getUserRoleById(int UserRolesId)
        {
            try{
                var response = await _userRolesService.getUserRolesById(UserRolesId);
                if(response == null)
                return NotFound("UserRole Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserRoles>> saveUserRole(UserRolesDTO userRolesDTO)
        {
            try{
                var response = await _userRolesService.saveUserRoles(userRolesDTO);
                if(response == null)
                return NotFound("User or Role Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{UserRolesId}")]
        public async Task<ActionResult<UserRoles>> updateUserRole(int UserRolesId,UserRolesDTO userRolesDTO)
        {
            try{
                var response = await _userRolesService.updateUserRoles(UserRolesId,userRolesDTO);
                if(response == null)
                return NotFound("User or Role Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{UserRolesId}")]
        public async Task<ActionResult<bool>> deleteUserRole(int UserRolesId)
        {
            try{
                var response = await _userRolesService.deleteUserRoles(UserRolesId);
                if(response)
                return Ok("UserRole Deleted Successfully");
                else
                return NotFound("UserRole Not Found");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}