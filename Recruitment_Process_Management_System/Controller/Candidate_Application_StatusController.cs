using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Candidate_Application_StatusController : ControllerBase
    {
        private readonly ICandidate_Application_StatusService _candidate_Application_StatusService;

        public Candidate_Application_StatusController(ICandidate_Application_StatusService candidate_Application_StatusService)
        {
            _candidate_Application_StatusService = candidate_Application_StatusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate_Application_Status>>> getAllCandidate_Application_Status()
        {
            try{
                var response = await _candidate_Application_StatusService.getAllCandidate_Application_Status();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Candidate_Application_Status_id}")]
        public async Task<ActionResult<Candidate_Application_Status>> getCandidate_Application_StatusById(int Candidate_Application_Status_id)
        {
            try{
                var response = await _candidate_Application_StatusService.getCandidate_Application_StatusById(Candidate_Application_Status_id);
                if(response == null)
                return NotFound("Candidate Application Status Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Candidate_Application_Status>> saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _candidate_Application_StatusService.saveCandidate_Application_Status(candidate_Application_StatusDTO);
                if(response == null)
                return BadRequest("Candidate Application is Already present or Candidate or Job or Application Status Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{Candidate_Application_Status_id}")]
        public async Task<ActionResult<Candidate_Application_Status>> updateCandidate_Application_Status(int Candidate_Application_Status_id,Candidate_Application_StatusDTO candidate_Application_StatusDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _candidate_Application_StatusService.updateCandidate_Application_Status(Candidate_Application_Status_id,candidate_Application_StatusDTO);
                if(response == null)
                return BadRequest("Candidate Application or Candidate or Job or Application Status Not Found");
                else
                return Ok(response); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{Candidate_Application_Status_id}")]
        public async Task<ActionResult<Candidate_Application_Status>> deleteCandidate_Application_Status(int Candidate_Application_Status_id)
        {
            try{
                var response = await _candidate_Application_StatusService.deleteCandidate_Application_Status(Candidate_Application_Status_id);
                if(response)
                return Ok("Candidate Application Deleted Successfully");
                else
                return NotFound("Candidate Application Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}