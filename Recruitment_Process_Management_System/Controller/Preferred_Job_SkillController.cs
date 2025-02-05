using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Preferred_Job_SkillController : ControllerBase
    {
        private readonly IPreferred_Job_SkillService _preferred_Job_SkillService;

        public Preferred_Job_SkillController(IPreferred_Job_SkillService preferred_Job_SkillService)
        {
            _preferred_Job_SkillService = preferred_Job_SkillService;
        }

        [HttpGet]
         public ActionResult<IEnumerable<Preferred_Job_Skill>> getAllPreferred_Job_Skill()
        {
            var response = _preferred_Job_SkillService.getAllPreferred_Job_Skill();
            return Ok(response);
        }

        [HttpGet("{Preferred_Job_Skill_id}")]
         public ActionResult<Preferred_Job_Skill> getPreferred_Job_SkillById(int Preferred_Job_Skill_id)
        {
            var response = _preferred_Job_SkillService.getPreferred_Job_SkillById(Preferred_Job_Skill_id);
            if(response == null)
            return NotFound("Preferred Job Skill Not Found");
            else
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<Preferred_Job_Skill> savePreferred_Job_Skill(Preferred_Job_SkillDTO preferred_Job_SkillDTO)
        {
            var response = _preferred_Job_SkillService.savePreferred_Job_Skill(preferred_Job_SkillDTO);
            if(response == null)
            return NotFound("Job or Skill Not Found");
            else
            return Ok(response);
        }

        [HttpPut("{Preferred_Job_Skill_id}")]
        public ActionResult<Preferred_Job_Skill> updatePreferred_Job_Skill(int Preferred_Job_Skill_id,Preferred_Job_SkillDTO preferred_Job_SkillDTO)
        {
            var response = _preferred_Job_SkillService.updatePreferred_Job_Skill(Preferred_Job_Skill_id,preferred_Job_SkillDTO);
            if(response == null)
            return NotFound("Job or Skill Not Found");
            else
            return Ok(response);
        }

        [HttpDelete("{Preferred_Job_Skill_id}")]
        public ActionResult<bool> deletePreferred_Job_Skill(int Preferred_Job_Skill_id)
        {
            var response = _preferred_Job_SkillService.deletePreferred_Job_Skill(Preferred_Job_Skill_id);
            if(response)
            return Ok("Preferred Job Skill Deleted Successfully");
            else
            return NotFound("Preferred Job Skill Not Found");
        }
    }
}