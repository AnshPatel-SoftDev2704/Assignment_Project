using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewerController : ControllerBase
    {
        private readonly IInterviewerService _interviewerService;
        private readonly IInterviewService _interviewService;
        private readonly IUserService _userService;

        public InterviewerController(IInterviewerService interviewerService,IInterviewService interviewService,IUserService userService)
        {
            _interviewerService = interviewerService;
            _interviewService = interviewService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Interviewer>> getAllInterviewer()
        {
            var responses = _interviewerService.getAllInterviewer();
            return Ok(responses);
        }

        [HttpGet("{Interviewer_id}")]
        public ActionResult<Interviewer> getInterviewerById(int Interviewer_id)
        {
            var response = _interviewerService.getInterviewerById(Interviewer_id);
            if(response == null)
            return NotFound("Interviewer Not Found");
            else
            return Ok(response);
        }
        
        [HttpPost]
        public ActionResult<Interviewer> saveInterviewer(InterviewerDTO interviewerDTO)
        {
            var interview = _interviewService.getInterviewById(interviewerDTO.Interview_id);
            if(interview == null)
            return NotFound("Interview Not Found");
            
            var user = _userService.getUserById(interviewerDTO.User_id);
            if(user == null)
            return NotFound("User Not Found");

            var response = _interviewerService.saveInterviewer(interviewerDTO);
            return Ok(response);
        }

        [HttpPut("{Interviewer_id}")]
        public ActionResult<Interviewer> updateInterviewer(int Interviewer_id,InterviewerDTO interviewerDTO)
        {
            var interview = _interviewService.getInterviewById(interviewerDTO.Interview_id);
            if(interview == null)
            return NotFound("Interview Not Found");
            
            var user = _userService.getUserById(interviewerDTO.User_id);
            if(user == null)
            return NotFound("User Not Found");

            var interviewer = _interviewerService.updateInterviewer(Interviewer_id,interviewerDTO);
            if(interviewer == null)
            return NotFound("Interviewer Not Found");
            else
            return Ok(interviewer);
        }

        [HttpDelete("{Interviewer_id}")]
        public ActionResult<bool> deleteInterviewer(int Interviewer_id)
        {
            var response = _interviewerService.deleteInterviewer(Interviewer_id);
            if(response)
            return Ok("Interviewer Deleted Successfully");
            else
            return NotFound("Interviewer Not Found");
        }
    }
}