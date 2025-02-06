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
        public ActionResult<IEnumerable<Candidate_Details>> getAllCandidate_Details()
        {
            var candidates = _candidate_DetailsService.getAllCandidate_Details();
            return Ok(candidates);
        }

        [HttpGet("{Candidate_id}")]
        public ActionResult<Candidate_Details> getCandidate_DetailsById(int Candidate_id)
        {
            var candidate = _candidate_DetailsService.getCandidate_DetailsById(Candidate_id);
            if(candidate == null)
            return NotFound("Candiate Details Not Found");
            else
            return Ok(candidate);
        }

        [HttpPost]
        public ActionResult<Candidate_Details> saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO)
        {
            // if(this.ModelState.IsValid)
            // {
                Console.WriteLine(candidate_DetailsDTO.Candidate_name);
                var candidate = _candidate_DetailsService.saveCandidate_Details(candidate_DetailsDTO);
                if(candidate == null)
                return NotFound("Role Not Found");
                else
                return Ok(candidate);
            // }
            // else
            // return BadRequest();
        }

        [HttpPut("{Candidate_id}")]
        public ActionResult<Candidate_Details> updateCandidate_Details(int Candidate_id,Candidate_DetailsDTO candidate_DetailsDTO)
        {
            if(this.ModelState.IsValid)
            {
                var candidate = _candidate_DetailsService.updateCandidate_Details(Candidate_id,candidate_DetailsDTO);
                if(candidate == null)
                return NotFound("Role Not Found");
                else
                return Ok(candidate);
            }
            else
            return BadRequest();
        }
        
        [HttpDelete("{Candidate_id}")]
        public ActionResult<Candidate_Details> deleteCandidate_Details(int Candidate_id)
        {
            var response = _candidate_DetailsService.deleteCandidate_Details(Candidate_id);
            if(response)
            return Ok("Candidate Deleted Successfully");
            else
            return NotFound("Candidate Not Found");
        }
    }
}