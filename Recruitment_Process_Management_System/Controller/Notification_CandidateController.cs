using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Notification_CandidateController : ControllerBase
    {
        private readonly INotification_CandidateService _notification_CandidateService;

        public Notification_CandidateController(INotification_CandidateService notification_CandidateService)
        {
            _notification_CandidateService = notification_CandidateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notifications_Candidate>>> getAllCandidateNotifications()
        {
            try
            {
                var responses = await _notification_CandidateService.getAllCandidateNotifications();
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Notificaion_Candidate_id}")]
        public async Task<ActionResult<Notifications_Candidate>> getCandidateNotificationById(int Notificaion_Candidate_id)
        {
            try
            {
                var response = await _notification_CandidateService.getCandidateNotificationById(Notificaion_Candidate_id);
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

        [HttpPost]
        public async Task<ActionResult<Notifications_Candidate>> saveCandidateNotification(Notifications_CandidateDTO notifications_CandidateDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try
            {
                var response = await _notification_CandidateService.saveCandidateNotification(notifications_CandidateDTO);
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

        [HttpPut("{Notification_Candidate_id}")]
        public async Task<ActionResult<Notifications_Candidate>> updateUserNotification(int Notification_Candidate_id,Notifications_CandidateDTO notifications_CandidateDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try
            {
                var response = await _notification_CandidateService.updateCandidateNotification(Notification_Candidate_id,notifications_CandidateDTO);
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

        [HttpDelete("{Notification_Candidate_id}")]
        public async Task<ActionResult<Notifications_Candidate>> deleteUserNotification(int Notification_Candidate_id)
        {
            try
            {
                var response = await _notification_CandidateService.deleteCandidateNotification(Notification_Candidate_id);
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