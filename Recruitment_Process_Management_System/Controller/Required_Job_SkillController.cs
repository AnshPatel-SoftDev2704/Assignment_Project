using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensibility;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Required_Job_SkillController : ControllerBase
    {
        private readonly IRequired_Job_SkillService _required_Job_SkillService;
        public Required_Job_SkillController(IRequired_Job_SkillService required_Job_SkillService)
        {
            _required_Job_SkillService = required_Job_SkillService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Required_Job_Skill>> getAllRequired_Job_Skill()
        {
            var response = _required_Job_SkillService.getAllRequired_Job_Skill();
            return Ok(response);
        }

        [HttpGet("{Required_Job_Skill_id}")]
        public ActionResult<Required_Job_Skill> getRequired_Job_SkillById(int Required_Job_Skill_id)
        {
            var response = _required_Job_SkillService.getRequired_Job_SkillById(Required_Job_Skill_id);
            if(response == null)
            return NotFound("Required Job Skill Not Found");
            else
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<Required_Job_Skill> saveRequired_Job_Skill(Required_Job_SkillDTO required_Job_SkillDTO)
        {
            var response = _required_Job_SkillService.saveRequired_Job_Skill(required_Job_SkillDTO);
            if(response == null)
            return NotFound("Job or Skill Not Found");
            else
            return Ok(response);
        }

        [HttpPut("{Required_Job_Skill_id}")]
        public ActionResult<Required_Job_Skill> updateRequired_Job_Skill(int Required_Job_Skill_id,Required_Job_SkillDTO required_Job_SkillDTO)
        {
            var response = _required_Job_SkillService.updateRequired_Job_Skill(Required_Job_Skill_id,required_Job_SkillDTO);
            if(response == null)
            return NotFound("Job or Skill Not Found");
            else
            return Ok(response);
        }

        [HttpDelete("{Required_Job_Skill_id}")]
        public ActionResult<bool> deleteRequired_Job_Skill(int Required_Job_Skill_id)
        {
            var response = _required_Job_SkillService.deleteRequired_Job_Skill(Required_Job_Skill_id);
            if(response)
            return Ok("Required Job Skill Deleted Successfully");
            else
            return NotFound("Required Job Skill Not Found");
        }
    }
}