using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> getAllFeedback()
        {
            try
            {
                var responses = await _feedbackService.getAllFeedback();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Feedback_id}")]
        public async Task<ActionResult<Feedback>> getFeedbackById(int Feedback_id)
        {
            try{
                var response = await _feedbackService.getFeedbackById(Feedback_id);
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

        [HttpPost]
        public async Task<ActionResult<Feedback>> saveFeedback(FeedbackDTO feedbackDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);

            try{
                var response = await _feedbackService.saveFeedback(feedbackDTO);
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

        [HttpPut("{Feedback_id}")]
        public async Task<ActionResult<Feedback>> updateFeedback(int Feedback_id,FeedbackDTO feedbackDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);

            try{
                var response = await _feedbackService.updateFeedback(Feedback_id,feedbackDTO);
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

        [HttpDelete("{Feedback_id}")]
        public async Task<ActionResult<Feedback>> deleteFeedback(int Feedback_id)
        {
            try{
                var response = await _feedbackService.deleteFeedback(Feedback_id);
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