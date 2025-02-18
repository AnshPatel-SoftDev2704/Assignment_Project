using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,HR,Interviewer,Recruiter,Candidate")]
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _interviewService;
        public InterviewController(IInterviewService interviewService)
        {
            _interviewService = interviewService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR,Interviewer,Recruiter,Candidate")]
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

        [HttpGet("GetAllInterviewStatus")]
        public async Task<ActionResult<IEnumerable<Interview_Status>>> getAllInterview_Status()
        {
            try{
                var response = await _interviewService.getAllInterview_Status();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInterviewStatusById/{Interview_Status_id}")]
        public async Task<ActionResult<Interview_Status>> getInterview_StatusById(int Interview_Status_id)
        {
            try{
                var response = await _interviewService.getInterview_StatusById(Interview_Status_id);
                if(response == null)
                return NotFound("Interview Status Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("SaveInterviewStatus")]
        public async Task<ActionResult<Interview_Status>> saveInterview_Status(Interview_Status interview_Status)
        {
            try{
                var response = await _interviewService.saveInterview_Status(interview_Status);
                if(response == null)
                return NotFound("Something Went Wrong.");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllInterviewType")]
        public async Task<ActionResult<IEnumerable<Interview_Type>>> getAllInterview_Type()
        {
            try{
                var response = await _interviewService.getAllInterview_Type();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInterviewTypeById/{Interview_Type_id}")]
        public async Task<ActionResult<Interview_Type>> getInterview_TypeById(int Interview_Type_id)
        {
            try{
                var response = await _interviewService.getInterview_TypeById(Interview_Type_id);
                if(response == null)
                return NotFound("Interview Type Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("SaveInterviewType")]
        public async Task<ActionResult<Interview_Type>> saveInterview_Type(Interview_Type interview_Type)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _interviewService.saveInterview_Type(interview_Type);
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

        [HttpGet("GetAllInterviewer")]
        public async Task<ActionResult<IEnumerable<Interviewer>>> getAllInterviewer()
        {
            try{
                var responses = await _interviewService.getAllInterviewer();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInterviewerById/{Interviewer_id}")]
        public async Task<ActionResult<Interviewer>> getInterviewerById(int Interviewer_id)
        {
            try{
                var response = await _interviewService.getInterviewerById(Interviewer_id);
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
        
        [HttpPost("SaveInterviewer")]
        public async Task<ActionResult<Interviewer>> saveInterviewer(InterviewerDTO interviewerDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
             try{
                var response = await _interviewService.saveInterviewer(interviewerDTO);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("UpdateInterviewer{Interviewer_id}")]
        public async Task<ActionResult<Interviewer>> updateInterviewer(int Interviewer_id,InterviewerDTO interviewerDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var interviewer = await _interviewService.updateInterviewer(Interviewer_id,interviewerDTO);
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

        [HttpDelete("DeleteInterviewer/{Interviewer_id}")]
        public async Task<ActionResult<bool>> deleteInterviewer(int Interviewer_id)
        {
            try{
                var response = await _interviewService.deleteInterviewer(Interviewer_id);
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

        [HttpGet("GetAllFeedback")]
        public async Task<ActionResult<IEnumerable<Feedback>>> getAllFeedback()
        {
            try
            {
                var responses = await _interviewService.getAllFeedback();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFeedbackById/{Feedback_id}")]
        public async Task<ActionResult<Feedback>> getFeedbackById(int Feedback_id)
        {
            try{
                var response = await _interviewService.getFeedbackById(Feedback_id);
                if(response == null)
                return NotFound("Feedback Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveFeedback")]
        [Authorize(Roles = "HR,Interviewer")]
        public async Task<ActionResult<Feedback>> saveFeedback(FeedbackDTO feedbackDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);

            try{
                var response = await _interviewService.saveFeedback(feedbackDTO);
                if(response == null)
                return NotFound("Something Went wrong");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateFeedback/{Feedback_id}")]
        [Authorize(Roles = "HR,Interviewer")]
        public async Task<ActionResult<Feedback>> updateFeedback(int Feedback_id,FeedbackDTO feedbackDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);

            try{
                var response = await _interviewService.updateFeedback(Feedback_id,feedbackDTO);
                if(response == null)
                return NotFound("Something Went wrong");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteFeedback/{Feedback_id}")]
        public async Task<ActionResult<Feedback>> deleteFeedback(int Feedback_id)
        {
            try{
                var response = await _interviewService.deleteFeedback(Feedback_id);
                if(!response)
                return NotFound("Something Went wrong");
                else
                return Ok("Feedback Deleted Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}