using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,HR,Interviewer")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            
        [Authorize(Roles ="Admin,HR,Interviewer")]
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

        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> getUser(string username)
        {
            try{
                var response = await _userService.getUser(username);
                if(response == null)
                return NotFound("User Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllUserRoles")]
        public async Task<ActionResult<IEnumerable<UserRoles>>> getAllUserRole()
        {
            try{
                var userRoles = await _userService.getAllUserRoles();
                return Ok(userRoles);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getUserRolesById/{UserRolesId}")]
        public async Task<ActionResult<IEnumerable<UserRoles>>> getUserRoleById(int UserRolesId)
        {
            try{
                var response = await _userService.getUserRolesById(UserRolesId);
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

        [HttpPost("saveUserRoles")]
        public async Task<ActionResult<UserRoles>> saveUserRole(UserRolesDTO userRolesDTO)
        {
            try{
                Console.WriteLine(userRolesDTO.User_id);
                Console.WriteLine(userRolesDTO.Role_id);
                var response = await _userService.saveUserRoles(userRolesDTO);
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

        [HttpPut("updateUserRole/{UserRolesId}")]
        public async Task<ActionResult<UserRoles>> updateUserRole(int UserRolesId,UserRolesDTO userRolesDTO)
        {
            try{
                var response = await _userService.updateUserRoles(UserRolesId,userRolesDTO);
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

        [HttpDelete("deleteUserRole/{UserRolesId}")]
        public async Task<ActionResult<bool>> deleteUserRole(int UserRolesId)
        {
            try{
                var response = await _userService.deleteUserRoles(UserRolesId);
                if(response)
                return Ok("UserRole Deleted Successfully");
                else
                return NotFound("UserRole Not Found");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> getAllRole()
        {
            try{
                var roles = await _userService.getAllRole();
                return Ok(roles);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getRoleById/{getRoleById}")]
        public async Task<ActionResult<Role>> getRoleById(int getRoleById)
        {
            try{
                var role = await _userService.getRoleById(getRoleById);
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

        [HttpPost("saveRole")]
        public async Task<ActionResult<Role>> saveRole(Role role)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _userService.saveRole(role);
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
    }
}