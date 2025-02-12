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

        [HttpGet("getAllJobStatus")]
        public async Task<ActionResult<IEnumerable<Job_Status>>> getAllJobStatus()
        {
            try{
                var jobStautses = await _jobsSerivce.getAllJobStatus();
                return Ok(jobStautses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getJobStatusById/{Job_Status_id}")]
        public async Task<ActionResult<Job_Status>> getJobStatusById(int Job_Status_id)
        {
            try{
                var jobStatus = await _jobsSerivce.getJobStatusById(Job_Status_id);
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

        [HttpPost("SaveJobStatus")]
        public async Task<ActionResult<Job_Status>> saveJobStatus(Job_Status jobStatus)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _jobsSerivce.saveJobStatus(jobStatus);
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

        [HttpGet("GetAllRequiredJobSkill")]
        public async Task<ActionResult<IEnumerable<Required_Job_Skill>>> getAllRequired_Job_Skill()
        {
            try{
                var response = await _jobsSerivce.getAllRequired_Job_Skill();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRequiredJobSkillById/{Required_Job_Skill_id}")]
        public async Task<ActionResult<Required_Job_Skill>> getRequired_Job_SkillById(int Required_Job_Skill_id)
        {
            try{
                var response = await _jobsSerivce.getRequired_Job_SkillById(Required_Job_Skill_id);
                if(response == null)
                return NotFound("Required Job Skill Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveRequiredJobSkill")]
        public async Task<ActionResult<Required_Job_Skill>> saveRequired_Job_Skill(Required_Job_SkillDTO required_Job_SkillDTO)
        {
            try{
                var response = await _jobsSerivce.saveRequired_Job_Skill(required_Job_SkillDTO);
                if(response == null)
                return NotFound("Job or Skill Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("updateRequiredJobSkill/{Required_Job_Skill_id}")]
        public async Task<ActionResult<Required_Job_Skill>> updateRequired_Job_Skill(int Required_Job_Skill_id,Required_Job_SkillDTO required_Job_SkillDTO)
        {
            try{
                 var response = await _jobsSerivce.updateRequired_Job_Skill(Required_Job_Skill_id,required_Job_SkillDTO);
                if(response == null)
                return NotFound("Job or Skill Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("deleteRequiredJobSkill/{Required_Job_Skill_id}")]
        public async Task<ActionResult<bool>> deleteRequired_Job_Skill(int Required_Job_Skill_id)
        {
            try{
                var response = await _jobsSerivce.deleteRequired_Job_Skill(Required_Job_Skill_id);
                if(response)
                return Ok("Required Job Skill Deleted Successfully");
                else
                return NotFound("Required Job Skill Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllPreferredJobSkill")]
         public async Task<ActionResult<IEnumerable<Preferred_Job_Skill>>> getAllPreferred_Job_Skill()
        {
            try{
                var response = await _jobsSerivce.getAllPreferred_Job_Skill();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPreferredJobSkillById/{Preferred_Job_Skill_id}")]
         public async Task<ActionResult<Preferred_Job_Skill>> getPreferred_Job_SkillById(int Preferred_Job_Skill_id)
        {
            try{
                var response = await _jobsSerivce.getPreferred_Job_SkillById(Preferred_Job_Skill_id);
                if(response == null)
                return NotFound("Preferred Job Skill Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SavePreferredJobSkill")]
        public async Task<ActionResult<Preferred_Job_Skill>> savePreferred_Job_Skill(Preferred_Job_SkillDTO preferred_Job_SkillDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _jobsSerivce.savePreferred_Job_Skill(preferred_Job_SkillDTO);
                if(response == null)
                return NotFound("Job or Skill Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("UpdatePreferredJobSkill/{Preferred_Job_Skill_id}")]
        public async Task<ActionResult<Preferred_Job_Skill>> updatePreferred_Job_Skill(int Preferred_Job_Skill_id,Preferred_Job_SkillDTO preferred_Job_SkillDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _jobsSerivce.updatePreferred_Job_Skill(Preferred_Job_Skill_id,preferred_Job_SkillDTO);
                if(response == null)
                return NotFound("Job or Skill Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePreferredJobSkill/{Preferred_Job_Skill_id}")]
        public async Task<ActionResult<bool>> deletePreferred_Job_Skill(int Preferred_Job_Skill_id)
        {
            try{
                var response = await _jobsSerivce.deletePreferred_Job_Skill(Preferred_Job_Skill_id);
                if(response)
                return Ok("Preferred Job Skill Deleted Successfully");
                else
                return NotFound("Preferred Job Skill Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}