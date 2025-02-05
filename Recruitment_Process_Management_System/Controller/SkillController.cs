using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Skill>> getAllSkill()
        {
            var skills = _skillService.getAllSkill();
            return Ok(skills);
        }

        [HttpGet("{Skill_id}")]
        public ActionResult<Skill> getSkillById(int Skill_id)
        {
            var skill = _skillService.getSkillById(Skill_id);
            if(skill == null)
            return NotFound(null);
            else
            return Ok(skill);
        }

        [HttpPost]
        public ActionResult<Skill> saveSkill(Skill skill)
        {
            var response = _skillService.saveSkill(skill);
            if(response == null)
            return BadRequest(null);
            else
            return Ok(response);
        }

        [HttpPut("{Skill_id}")]
        public ActionResult<Skill> updateSkill(int Skill_id,Skill skill)
        {
            var response = _skillService.updateSkill(Skill_id,skill);
            if(response == null)
            return NotFound(null);
            else
            return Ok(response);
        }

        [HttpDelete("{Skill_id}")]
        public ActionResult<bool> deleteSkill(int Skill_id)
        {
            var response = _skillService.deleteSkill(Skill_id);
            if(response)
            return Ok();
            else
            return NotFound();
        }

    }
}