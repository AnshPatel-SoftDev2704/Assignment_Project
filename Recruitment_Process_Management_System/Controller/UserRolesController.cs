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
        public ActionResult<IEnumerable<UserRoles>> getAllUserRole()
        {
            var userRoles = _userRolesService.getAllUserRoles();
            return Ok(userRoles);
        }

        [HttpGet("{UserRolesId}")]
        public ActionResult<UserRoles> getUserRoleById(int UserRolesId)
        {
            var response = _userRolesService.getUserRolesById(UserRolesId);
            if(response == null)
            return NotFound("UserRole Not Found");
            else
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<UserRoles> saveUserRole(UserRolesDTO userRolesDTO)
        {
            var response = _userRolesService.saveUserRoles(userRolesDTO);
            if(response == null)
            return NotFound("User or Role Not Found");
            else
            return Ok(response);
        }

        [HttpPut("{UserRolesId}")]
        public ActionResult<UserRoles> updateUserRole(int UserRolesId,UserRolesDTO userRolesDTO)
        {
            var response = _userRolesService.updateUserRoles(UserRolesId,userRolesDTO);
            if(response == null)
            return NotFound("User or Role Not Found");
            else
            return Ok(response);
        }

        [HttpDelete("{UserRolesId}")]
        public ActionResult<bool> deleteUserRole(int UserRolesId)
        {
            var response = _userRolesService.deleteUserRoles(UserRolesId);
            if(response)
            return Ok("UserRole Deleted Successfully");
            else
            return NotFound("UserRole Not Found");
        }
    }
}