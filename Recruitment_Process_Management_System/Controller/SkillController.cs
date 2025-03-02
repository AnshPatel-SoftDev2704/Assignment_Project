using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Candidate")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        [Authorize(Roles = "HR,Interviewer,Reviewer,Admin,Recruiter,Candidate")]
        public async Task<ActionResult<IEnumerable<Skill>>> getAllSkill()
        {
            try{
                var skills = await _skillService.getAllSkill();
                return Ok(skills);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Skill_id}")]
        public async Task<ActionResult<Skill>> getSkillById(int Skill_id)
        {
            try{
                var skill = await _skillService.getSkillById(Skill_id);
                if(skill == null)
                return NotFound(null);
                else
                return Ok(skill);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Candidate")]
        public async Task<ActionResult<Skill>> saveSkill(Skill skill)
        {
            try{
                var response = await _skillService.saveSkill(skill);
                if(response == null)
                return BadRequest(null);
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{Skill_id}")]
        public async Task<ActionResult<Skill>> updateSkill(int Skill_id,Skill skill)
        {
            try{
                var response = await _skillService.updateSkill(Skill_id,skill);
                if(response == null)
                return NotFound(null);
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetFile")]
        public ActionResult GetFile([FromQuery] string filepath)
        {
            Console.WriteLine(filepath);
            if (string.IsNullOrEmpty(filepath))
            {
                return BadRequest("File path is required.");
            }

            if (!System.IO.File.Exists(filepath))
            {
                return NotFound("File not found.");
            }

            var mimeType = "application/pdf";
            return PhysicalFile(filepath, mimeType);
        }
        [HttpDelete("{Skill_id}")]
        public async Task<ActionResult<bool>> deleteSkill(int Skill_id)
        {
            try{
                var response = await _skillService.deleteSkill(Skill_id);
                if(response)
                return Ok();
                else
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}