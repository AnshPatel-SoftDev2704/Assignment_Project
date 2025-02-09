using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Selected_CandidateController : ControllerBase
    {
        private readonly ISelected_CandidateService _selected_CandidateService;

        public Selected_CandidateController(ISelected_CandidateService selected_CandidateService)
        {
            _selected_CandidateService = selected_CandidateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Selected_Candidate>>> getAllSelectedCandidate()
        {
            try{
                var responses = await _selected_CandidateService.getAllSelectedCandidate();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Selected_Candidate_id}")]
        public async Task<ActionResult<Selected_Candidate>> getSelectedCandidateById(int Selected_Candidate_id)
        {
            try{
                var response = await _selected_CandidateService.getSelectedCadidateById(Selected_Candidate_id);
                if(response == null)
                return NotFound("Selected Candidate Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Selected_Candidate>> saveSelectedCandidate(Selected_CandidateDTO selected_CandidateDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _selected_CandidateService.saveSelectedCandidate(selected_CandidateDTO);
                if(response == null)
                return NotFound("Something went wrong.");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Selected_Candidate_id}")]
        public async Task<ActionResult<Selected_Candidate>> updateSelectedCandidate(int Selected_Candidate_id,Selected_CandidateDTO selected_CandidateDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _selected_CandidateService.updateSelectedCandidate(Selected_Candidate_id,selected_CandidateDTO);
                if(response == null)
                return NotFound("Selected Candidate Not Found.");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Selected_Candidate_id}")]
        public async Task<ActionResult<Selected_Candidate>> deleteSelectedCandidate(int Selected_Candidate_id)
        {
            try{
                var response = await _selected_CandidateService.deleteSelectedCandidate(Selected_Candidate_id);
                if(!response)
                return NotFound("Selected Candidate Not Found.");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}