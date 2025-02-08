using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<Jobs>>> getAllJobs()
        {
            try{
                var jobs = await _jobsSerivce.getAllJobs();
                return Ok(jobs);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Job_id}")]
        public async Task<ActionResult<Jobs>> getJobById(int Job_id)
        {
            try{
                var job = await _jobsSerivce.getJobById(Job_id);
                if(job == null)
                return NotFound("Job Not Found");
                else
                return Ok(job);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpPost]
        public async Task<ActionResult<Jobs>> saveJobs(JobsDTO jobsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _jobsSerivce.saveJob(jobsDTO);
                if(response == null)
                return NotFound("User or Job Status Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpPut("{Job_id}")]
        public async Task<ActionResult<Jobs>> updateJob(int Job_id,JobsDTO jobsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _jobsSerivce.updateJob(Job_id,jobsDTO);
                if(response == null)
                return NotFound("User or Job Status Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }
        
        [HttpDelete("{Job_id}")]
        public async Task<ActionResult<bool>> deleteJob(int Job_id)
        {
            try{
                var response = await _jobsSerivce.deleteJob(Job_id);
                if(response)
                return Ok("Job Delete Successfully");
                else
                return NotFound("Job Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}