using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService,IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet("getAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            try{
                var users = await _userService.getAllUser();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getUserById/{userId}")]
        public async Task<ActionResult<User>> getUserById(int userId){
            try{
                var user = await _userService.getUserById(userId);
                if(user == null)
                return NotFound(null);
                else
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("saveUser")]
        public async Task<ActionResult<User>> saveUserAsync(UserDTO userDTO)
        {
            try{
                var user1 = await _userService.saveUser(userDTO);
                return Ok(user1);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("updateUser/{userId}")]
        public async Task<ActionResult<User>> updateUser(int userId,UserDTO userDTO)
        {
            try{
                var user = await _userService.updateUser(userId,userDTO);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteUser/{userId}")]
        public async Task<ActionResult<User>> deleteUser(int userId)
        {
            try{
                var response = await _userService.deleteUser(userId);
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