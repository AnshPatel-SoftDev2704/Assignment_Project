using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Interview_StatusController : ControllerBase
    {
        private readonly IInterview_StatusService _interview_StatusService;

        public Interview_StatusController(IInterview_StatusService interview_StatusService)
        {
            _interview_StatusService = interview_StatusService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Interview_Status>> getAllInterview_Status()
        {
            var response = _interview_StatusService.getAllInterview_Status();
            return Ok(response);
        }

        [HttpGet("{Interview_Status_id}")]
        public ActionResult<Interview_Status> getInterview_StatusById(int Interview_Status_id)
        {
            var response = _interview_StatusService.getInterview_StatusById(Interview_Status_id);
            if(response == null)
            return NotFound("Interview Status Not Found");
            else
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<Interview_Status> saveInterview_Status(Interview_Status interview_Status)
        {
            var response = _interview_StatusService.saveInterview_Status(interview_Status);
            if(response == null)
            return NotFound("Something Went Wrong.");
            else
            return Ok(response);
        }

        [HttpPut("{Interview_Status_id}")]
        public ActionResult<Interview_Status> updateInterview_Status(int Interview_Status_id,Interview_Status interview_Status)
        {
            var response = _interview_StatusService.updateInterview_Status(Interview_Status_id,interview_Status);
            if(response == null)
            return NotFound("Interview Status Not Found");
            else
            return Ok(response);
        }

        [HttpDelete("{Interview_Status_id}")]
        public ActionResult<bool> deleteInterview_Status(int Interview_Status_id)
        {
            var response = _interview_StatusService.deleteInterview_Status(Interview_Status_id);
            if(response)
            return Ok("Interview Status Deleted Successfully");
            else
            return NotFound("Interview Status Not Found");
        }
    }
}