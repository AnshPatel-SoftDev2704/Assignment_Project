using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Notification_UsersController : ControllerBase
    {
        private readonly INotifications_UsersService _notifications_UsersService;

        public Notification_UsersController(INotifications_UsersService notifications_UsersService)
        {
            _notifications_UsersService = notifications_UsersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notifications_Users>>> getAllUserNotification()
        {
            try
            {
                var responses = await _notifications_UsersService.getAllUsersNotifications();
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Notificaion_User_id}")]
        public async Task<ActionResult<Notifications_Users>> getUserNotificationById(int Notificaion_User_id)
        {
            try
            {
                var response = await _notifications_UsersService.getUserNotificationById(Notificaion_User_id);
                if(response == null)
                return NotFound("User Notification Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Notifications_Users>> saveUserNotification(Notifications_UsersDTO notifications_UsersDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try
            {
                var response = await _notifications_UsersService.saveUserNotification(notifications_UsersDTO);
                if(response == null)
                return BadRequest("Something Went Wrong");
                else
                return Ok(response);
            }   
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Notification_User_id}")]
        public async Task<ActionResult<Notifications_Users>> updateUserNotification(int Notification_User_id,Notifications_UsersDTO notifications_UsersDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try
            {
                var response = await _notifications_UsersService.updateUserNotification(Notification_User_id,notifications_UsersDTO);
                if(response == null)
                return BadRequest("Something Went Wrong");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Notification_User_id}")]
        public async Task<ActionResult<Notifications_Users>> deleteUserNotification(int Notification_User_id)
        {
            try
            {
                var response = await _notifications_UsersService.deleteUserNotification(Notification_User_id);
                if(response)
                return Ok("User Notification Deleted Successfully");
                else
                return NotFound("User Notification Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllCandidateNotification")]
        public async Task<ActionResult<IEnumerable<Notifications_Candidate>>> getAllCandidateNotifications()
        {
            try
            {
                var responses = await _notifications_UsersService.getAllCandidateNotifications();
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCandidateNotificationById/{Notificaion_Candidate_id}")]
        public async Task<ActionResult<Notifications_Candidate>> getCandidateNotificationById(int Notificaion_Candidate_id)
        {
            try
            {
                var response = await _notifications_UsersService.getCandidateNotificationById(Notificaion_Candidate_id);
                if(response == null)
                return NotFound("Candidate Notification Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveCandidateNotification")]
        public async Task<ActionResult<Notifications_Candidate>> saveCandidateNotification(Notifications_CandidateDTO notifications_CandidateDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try
            {
                var response = await _notifications_UsersService.saveCandidateNotification(notifications_CandidateDTO);
                if(response == null)
                return BadRequest("Something Went Wrong");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCandidateNotification/{Notification_Candidate_id}")]
        public async Task<ActionResult<Notifications_Candidate>> updateCandidateNotification(int Notification_Candidate_id,Notifications_CandidateDTO notifications_CandidateDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try
            {
                var response = await _notifications_UsersService.updateCandidateNotification(Notification_Candidate_id,notifications_CandidateDTO);
                if(response == null)
                return BadRequest("Something Went Wrong");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCandidateNotification/{Notification_Candidate_id}")]
        public async Task<ActionResult<Notifications_Candidate>> deleteCandidateNotification(int Notification_Candidate_id)
        {
            try
            {
                var response = await _notifications_UsersService.deleteCandidateNotification(Notification_Candidate_id);
                if(response)
                return Ok("Candidate Notification Deleted Successfully");
                else
                return NotFound("Candidate Notification Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}