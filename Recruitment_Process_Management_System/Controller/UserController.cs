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
        public ActionResult<IEnumerable<User>> GetAllUser()
        {
            var users = _userService.getAllUser();
            return Ok(users);
        }

        [HttpGet("getUserById/{userId}")]
        public ActionResult<User> getUserById(int userId){
            var user = _userService.getUserById(userId);
            if(user == null)
            return NotFound(null);
            else
            return Ok(user);
        }

        [HttpPost("saveUser")]
        public ActionResult<User> saveUser([FromBody] UserDTO userDTO)
        {
            var user1 = _userService.saveUser(userDTO);
            return Ok(user1);
        }

        [HttpPut("updateUser/{userId}")]
        public ActionResult<User> updateUser(int userId,UserDTO userDTO)
        {
            var user = _userService.updateUser(userId,userDTO);
            return Ok(user);
            
        }

        [HttpDelete("deleteUser/{userId}")]
        public ActionResult<User> deleteUser(int userId)
        {
            var response = _userService.deleteUser(userId);
            if(response)
            return Ok();
            else
            return NotFound();
        }
    }
}