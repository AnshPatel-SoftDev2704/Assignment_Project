using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Candidate_SkillsController : ControllerBase
    {
        private readonly ICandidate_SkillService _candidate_SkillService;
        
        public Candidate_SkillsController(ICandidate_SkillService candidate_SkillService)
        {
            _candidate_SkillService = candidate_SkillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate_Skills>>> getAllCandidate_Skills()
        {
            try{
                var candidateSkills = await _candidate_SkillService.getAllCandidate_Skills();
                return Ok(candidateSkills);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Candidate_Skill_id}")]
        public async Task<ActionResult<Candidate_Skills>> getAllCandidate_SkillById(int Candidate_Skill_id)
        {
            try{
                var candidateSkill = await _candidate_SkillService.getAllCandidate_SkillById(Candidate_Skill_id);
                if(candidateSkill == null)
                return NotFound("Candidate Skill Not Found");
                else
                return Ok(candidateSkill);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Candidate_Skills>> saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var candidateSkill = await _candidate_SkillService.saveCandidate_Skill(candidate_SkillsDTO);
                if(candidateSkill == null)
                return BadRequest("Skill is Already present or Candidate or Skill Not Found");
                else
                return Ok(candidateSkill);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Candidate_Skill_id}")]
        public async Task<ActionResult<Candidate_Skills>> updateCandidate_Skill(int Candidate_Skill_id,Candidate_SkillsDTO candidate_SkillsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var candidateSkill = await _candidate_SkillService.updateCandidate_Skill(Candidate_Skill_id,candidate_SkillsDTO);
                if(candidateSkill == null)
                return BadRequest("Candidate Skill or Candidate or Skill Not Found");
                else
                return Ok(candidateSkill);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{Candidate_Skill_id}")]
        public async Task<ActionResult<Candidate_Skills>> deleteCandidate_Skill(int Candidate_Skill_id)
        {
            try{
                var response = await _candidate_SkillService.deleteCandidate_Skill(Candidate_Skill_id);
                if(response)
                return Ok("Candidate skill Deleted Successfully");
                else
                return NotFound("Candidate Skill Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}