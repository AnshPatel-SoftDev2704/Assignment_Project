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
        public ActionResult<IEnumerable<Job_Status>> getAllJobStatus()
        {
            var jobStautses = _jobStatusService.getAllJobStatus();
            return Ok(jobStautses);
        }

        [HttpGet("{Job_Status_id}")]
        public ActionResult<Job_Status> getJobStatusById(int Job_Status_id)
        {
            var jobStatus = _jobStatusService.getJobStatusById(Job_Status_id);
            if(jobStatus == null)
            return NotFound(null);
            else
            return Ok(jobStatus);
        }

        [HttpPost]
        public ActionResult<Job_Status> saveJobStatus(Job_Status jobStatus)
        {
            var response = _jobStatusService.saveJobStatus(jobStatus);
            if(response == null)
            return BadRequest("JobStatus is Already Present");
            else
            return Ok(response);
        }

        [HttpPut("{Job_Status_id}")]
        public ActionResult<Job_Status> updateJobStatus(int Job_Status_id,Job_Status jobStatus)
        {
            var response = _jobStatusService.updateJobStatus(Job_Status_id,jobStatus);
            if(response == null)
            return NotFound("JobStatus Does Not Exist.");
            else
            return Ok(response);  
        }

        [HttpDelete("{Job_Status_id}")]
        public ActionResult<bool> deleteJobStatus(int Job_Status_id)
        {
            var response = _jobStatusService.deleteJobStatus(Job_Status_id);
            if(response)
            return Ok("JobStatus Deleted Successfully");
            else
            return NotFound("JobStatus Does Not Exist");
        }
    }
}