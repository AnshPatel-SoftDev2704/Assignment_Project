using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> getAllRole()
        {
            var roles = _roleService.getAllRole();
            return Ok(roles);
        }

        [HttpGet("{Role_id}")]
        public ActionResult<Role> getRoleById(int Role_id)
        {
            var role = _roleService.getRoleById(Role_id);
            if(role != null)
            return Ok(role);
            else
            return NotFound(null);
        }

        [HttpPost]
        public ActionResult<Role> saveRole(Role role)
        {
            Console.WriteLine(role.Role_Description);
            var response = _roleService.saveRole(role);
            if(response != null)
            return Ok(response);
            else
            return BadRequest(null);
        }

        [HttpPut("{Role_id)}")]
        public ActionResult<Role> updateRole(int Role_id,Role role){
            var response = _roleService.updateRole(Role_id,role);
            if(response != null)
            return Ok(response);
            else
            return NotFound(response);
        }

        [HttpDelete("{Role_id}")]
        public ActionResult<bool> deleteRole(int Role_id)
        {
            var response = _roleService.deleteRole(Role_id);
            if(response)
            return Ok();
            else
            return NotFound();
        }
    }
}