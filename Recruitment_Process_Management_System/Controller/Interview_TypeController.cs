using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Repositories
{
    [ApiController]
    [Route("api/[controller]")]
    public class Interview_TypeController : ControllerBase
    {
        private readonly IInterview_TypeService _interview_TypeService;

        public Interview_TypeController(IInterview_TypeService interview_TypeService)
        {
            _interview_TypeService = interview_TypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interview_Type>>> getAllInterview_Type()
        {
            try{
                var response = await _interview_TypeService.getAllInterview_Type();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Interview_Type_id}")]
        public async Task<ActionResult<Interview_Type>> getInterview_TypeById(int Interview_Type_id)
        {
            try{
                var response = await _interview_TypeService.getInterview_TypeById(Interview_Type_id);
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

        [HttpPost]
        public async Task<ActionResult<Interview_Type>> saveInterview_Type(Interview_Type interview_Type)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _interview_TypeService.saveInterview_Type(interview_Type);
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

        [HttpPut("{Interview_Type_id}")]
        public async Task<ActionResult<Interview_Type>> updateInterview_Type(int Interview_Type_id,Interview_Type interview_Type)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _interview_TypeService.updateInterview_Type(Interview_Type_id,interview_Type);
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

        [HttpDelete("{Interview_Type_id}")]
        public async Task<ActionResult<Interview_Type>> deleteInterview_Type(int Interview_Type_id)
        {
            try{
                var response = await _interview_TypeService.deleteInterview_Type(Interview_Type_id);
                if(response)
                return Ok("Interview Type Deleted Successfully");
                else
                return NotFound("Interview Type Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}