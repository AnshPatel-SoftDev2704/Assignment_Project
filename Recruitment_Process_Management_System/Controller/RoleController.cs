using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> getAllRole()
        {
            try{
                var roles = await _roleService.getAllRole();
                return Ok(roles);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Role_id}")]
        public async Task<ActionResult<Role>> getRoleById(int Role_id)
        {
            try{
                var role = await _roleService.getRoleById(Role_id);
                if(role != null)
                return Ok(role);
                else
                return NotFound(null);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Role>> saveRole(Role role)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _roleService.saveRole(role);
                if(response != null)
                return Ok(response);
                else
                return BadRequest(null);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpPut("{Role_id)}")]
        public async Task<ActionResult<Role>> updateRole(int Role_id,Role role){
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);

            try{
                var response = await _roleService.updateRole(Role_id,role);
                if(response != null)
                return Ok(response);
                else
                return NotFound(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Role_id}")]
        public async Task<ActionResult<bool>> deleteRole(int Role_id)
        {
            try{
                var response = await _roleService.deleteRole(Role_id);
                if(response)
                return Ok();
                else
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}