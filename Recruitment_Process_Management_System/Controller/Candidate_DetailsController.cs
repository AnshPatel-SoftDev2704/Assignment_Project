using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Candidate_DetailsController : ControllerBase
    {
        private readonly ICandidate_DetailsService _candidate_DetailsService;
        public Candidate_DetailsController(ICandidate_DetailsService candidate_DetailsService)
        {
            _candidate_DetailsService = candidate_DetailsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate_Details>>> getAllCandidate_Details()
        {
            try{
                var candidates = await _candidate_DetailsService.getAllCandidate_Details();
                return Ok(candidates);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Candidate_id}")]
        public async Task<ActionResult<Candidate_Details>> getCandidate_DetailsById(int Candidate_id)
        {
            try{
                var candidate = await _candidate_DetailsService.getCandidate_DetailsById(Candidate_id);
                if(candidate == null)
                return NotFound("Candiate Details Not Found");
                else
                return Ok(candidate);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Candidate_Details>> saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var candidate = await _candidate_DetailsService.saveCandidate_Details(candidate_DetailsDTO);
                if(candidate == null)
                return NotFound("Role Not Found");
                else
                return Ok(candidate);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
                
        }

        [HttpPut("{Candidate_id}")]
        public async Task<ActionResult<Candidate_Details>> updateCandidate_Details(int Candidate_id,Candidate_DetailsDTO candidate_DetailsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var candidate = await _candidate_DetailsService.updateCandidate_Details(Candidate_id,candidate_DetailsDTO);
                if(candidate == null)
                return NotFound("Role Not Found");
                else
                return Ok(candidate);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
        [HttpDelete("{Candidate_id}")]
        public async Task<ActionResult<Candidate_Details>> deleteCandidate_Details(int Candidate_id)
        {
            try{
                var response = await  _candidate_DetailsService.deleteCandidate_Details(Candidate_id);
                if(response)
                return Ok("Candidate Deleted Successfully");
                else
                return NotFound("Candidate Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}