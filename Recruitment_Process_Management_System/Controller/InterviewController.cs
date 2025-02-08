using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<Interview>>> getAllInterview()
        {
            try{
                var response = await _interviewService.getAllInterview();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Interview_id}")]
        public async Task<ActionResult<Interview>> getInterviewById(int Interview_id)
        {
            try{
                var response = await _interviewService.getInterviewById(Interview_id);
                if(response == null)
                return NotFound("Interview Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Interview>> saveInterview(InterviewDTO interviewDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _interviewService.saveInterview(interviewDTO);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{Interview_id}")]
        public async Task<ActionResult<Interview>> updatInterview(int Interview_id,InterviewDTO interviewDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _interviewService.updateInterview(Interview_id,interviewDTO);
                if(response == null)
                return NotFound("Interview Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{Interview_id}")]
        public async Task<ActionResult<Interview>> deleteInterview(int Interview_id)
        {
            try{
                var response = await _interviewService.deleteInterview(Interview_id);
                if(response)
                return Ok("Interview Deleted Successfully");
                else
                return NotFound("Interview Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}