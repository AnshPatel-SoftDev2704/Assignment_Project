using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _interviewService;
        private readonly ICandidate_Application_StatusService _candidate_Application_StatusService;
        private readonly IInterview_TypeService _interview_TypeService;
        private readonly IInterview_StatusService _interview_StatusService;
        public InterviewController(IInterviewService interviewService,ICandidate_Application_StatusService candidate_Application_StatusService,IInterview_StatusService interview_StatusService,IInterview_TypeService interview_TypeService)
        {
            _interviewService = interviewService;
            _candidate_Application_StatusService = candidate_Application_StatusService;
            _interview_StatusService = interview_StatusService;
            _interview_TypeService = interview_TypeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Interview>> getAllInterview()
        {
            var response = _interviewService.getAllInterview();
            return Ok(response);
        }

        [HttpGet("{Interview_id}")]
        public ActionResult<Interview> getInterviewById(int Interview_id)
        {
            var response = _interviewService.getInterviewById(Interview_id);
            if(response == null)
            return NotFound("Interview Not Found");
            else
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<Interview> saveInterview(InterviewDTO interviewDTO)
        {
            var candidate_Application_Status = _candidate_Application_StatusService.getCandidate_Application_StatusById(interviewDTO.Application_id);
            if(candidate_Application_Status == null)
            return NotFound("Candidate Application Not Found");

            var interview_Type = _interview_TypeService.getInterview_TypeById(interviewDTO.Interview_Type_id);
            if(interview_Type == null)
            return NotFound("Interview Type Not Found");

            var interview_Status = _interview_StatusService.getInterview_StatusById(interviewDTO.Interview_Status_id);
            if(interview_Status == null)
            return NotFound("Interview Status Not Found");
            
            var response = _interviewService.saveInterview(interviewDTO);
            return Ok(response);
        }

        [HttpPut("{Interview_id}")]
        public ActionResult<Interview> updatInterview(int Interview_id,InterviewDTO interviewDTO)
        {
            var candidate_Application_Status = _candidate_Application_StatusService.getCandidate_Application_StatusById(interviewDTO.Application_id);
            if(candidate_Application_Status == null)
            return NotFound("Candidate Application Not Found");

            var interview_Type = _interview_TypeService.getInterview_TypeById(interviewDTO.Interview_Type_id);
            if(interview_Type == null)
            return NotFound("Interview Type Not Found");

            var interview_Status = _interview_StatusService.getInterview_StatusById(interviewDTO.Interview_Status_id);
            if(interview_Status == null)
            return NotFound("Interview Status Not Found");

            var response = _interviewService.updateInterview(Interview_id,interviewDTO);
            if(response == null)
            return NotFound("Interview Not Found");
            else
            return Ok(response);
        }

        [HttpDelete("{Interview_id}")]
        public ActionResult<Interview> deleteInterview(int Interview_id)
        {
            var response = _interviewService.deleteInterview(Interview_id);
            if(response)
            return Ok("Interview Deleted Successfully");
            else
            return NotFound("Interview Not Found");
        }
    }
}