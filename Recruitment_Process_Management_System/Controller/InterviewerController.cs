using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<Interviewer>>> getAllInterviewer()
        {
            try{
                var responses = await _interviewerService.getAllInterviewer();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Interviewer_id}")]
        public async Task<ActionResult<Interviewer>> getInterviewerById(int Interviewer_id)
        {
            try{
                var response = await _interviewerService.getInterviewerById(Interviewer_id);
                if(response == null)
                return NotFound("Interviewer Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
        [HttpPost]
        public async Task<ActionResult<Interviewer>> saveInterviewer(InterviewerDTO interviewerDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
             try{
                var response = await _interviewerService.saveInterviewer(interviewerDTO);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{Interviewer_id}")]
        public async Task<ActionResult<Interviewer>> updateInterviewer(int Interviewer_id,InterviewerDTO interviewerDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var interviewer = await _interviewerService.updateInterviewer(Interviewer_id,interviewerDTO);
                if(interviewer == null)
                return NotFound("Interviewer Not Found");
                else
                return Ok(interviewer);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Interviewer_id}")]
        public async Task<ActionResult<bool>> deleteInterviewer(int Interviewer_id)
        {
            try{
                var response = await _interviewerService.deleteInterviewer(Interviewer_id);
                if(response)
                return Ok("Interviewer Deleted Successfully");
                else
                return NotFound("Interviewer Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}