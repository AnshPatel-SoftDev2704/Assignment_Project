using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobsSerivce _jobsSerivce;

        public JobsController(IJobsSerivce jobsSerivce)
        {
            _jobsSerivce = jobsSerivce;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Jobs>> getAllJobs()
        {
            var jobs = _jobsSerivce.getAllJobs();
            return Ok(jobs);
        }

        [HttpGet("{Job_id}")]
        public ActionResult<Jobs> getJobById(int Job_id)
        {
            var job = _jobsSerivce.getJobById(Job_id);
            if(job == null)
            return NotFound("Job Not Found");
            else
            return Ok(job);
        }

        [HttpPost]
        public ActionResult<Jobs> saveJobs(JobsDTO jobsDTO)
        {
            var response = _jobsSerivce.saveJob(jobsDTO);
            if(response == null)
            return NotFound("User or Job Status Not Found");
            else
            return Ok(response);
        }

        [HttpPut("{Job_id}")]
        public ActionResult<Jobs> updateJob(int Job_id,JobsDTO jobsDTO)
        {
            var response = _jobsSerivce.updateJob(Job_id,jobsDTO);
            if(response == null)
            return NotFound("User or Job Status Not Found");
            else
            return Ok(response);
        }
        
        [HttpDelete("{Job_id}")]
        public ActionResult<bool> deleteJob(int Job_id)
        {
            var response = _jobsSerivce.deleteJob(Job_id);
            if(response)
            return Ok("Job Delete Successfully");
            else
            return NotFound("Job Not Found");
        }
    }
}