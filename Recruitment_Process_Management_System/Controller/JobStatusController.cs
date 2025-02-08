using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobStatusController : ControllerBase
    {
        private readonly IJobStatusService _jobStatusService;
        
        public JobStatusController(IJobStatusService jobStatusService)
        {
            _jobStatusService = jobStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job_Status>>> getAllJobStatus()
        {
            try{
                var jobStautses = await _jobStatusService.getAllJobStatus();
                return Ok(jobStautses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Job_Status_id}")]
        public async Task<ActionResult<Job_Status>> getJobStatusById(int Job_Status_id)
        {
            try{
                var jobStatus = await _jobStatusService.getJobStatusById(Job_Status_id);
                if(jobStatus == null)
                return NotFound(null);
                else
                return Ok(jobStatus);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Job_Status>> saveJobStatus(Job_Status jobStatus)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _jobStatusService.saveJobStatus(jobStatus);
                if(response == null)
                return BadRequest("JobStatus is Already Present");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Job_Status_id}")]
        public async Task<ActionResult<Job_Status>> updateJobStatus(int Job_Status_id,Job_Status jobStatus)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _jobStatusService.updateJobStatus(Job_Status_id,jobStatus);
                if(response == null)
                return NotFound("JobStatus Does Not Exist.");
                else
                return Ok(response);  
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Job_Status_id}")]
        public async Task<ActionResult<bool>> deleteJobStatus(int Job_Status_id)
        {
            try{
                var response = await _jobStatusService.deleteJobStatus(Job_Status_id);
                if(response)
                return Ok("JobStatus Deleted Successfully");
                else
                return NotFound("JobStatus Does Not Exist");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}