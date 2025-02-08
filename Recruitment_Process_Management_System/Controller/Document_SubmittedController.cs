using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Document_SubmittedController : ControllerBase
    {
        private readonly IDocument_SubmittedService? _document_SubmittedService;
        private readonly ICandidate_DetailsService _candidate_DetailsService;
        private readonly IJobsSerivce _jobsSerivce;
        private readonly IDocument_TypeService _document_TypeService;
        private readonly IUserService _userService;

        public Document_SubmittedController(IDocument_SubmittedService document_SubmittedService,ICandidate_DetailsService candidate_DetailsService,IJobsSerivce jobsSerivce,IDocument_TypeService document_TypeService,IUserService userService)
        {
            _document_SubmittedService = document_SubmittedService;
            _candidate_DetailsService = candidate_DetailsService;
            _jobsSerivce = jobsSerivce;
            _document_TypeService = document_TypeService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document_Submitted>>> getAllDocument_Submitted()
        {
            try{
                var responses = await _document_SubmittedService.getAllDocument_Submitted();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Document_Submitted_id}")]
        public async Task<ActionResult<Document_Submitted>> getDocument_SubmittedById(int Document_Submitted_id)
        {
            try{
                var response = await _document_SubmittedService.getDocument_SubmittedById(Document_Submitted_id);
                if(response == null)
                return NotFound("Submitted Document Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Document_Submitted>> saveDocument_Submitted(Document_SubmittedDTO document_SubmittedDTO)
        {
            var newCandidate = await  _candidate_DetailsService.getCandidate_DetailsById(document_SubmittedDTO.Candidate_id);
            if(newCandidate == null)
            return NotFound("Candidate Not Found");
            
            var newJob = await _jobsSerivce.getJobById(document_SubmittedDTO.Job_id);
            if(newJob == null)
            return NotFound("Job Not Found");

            var newDocument_Type = await _document_TypeService.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
            if(newDocument_Type == null)
            return NotFound("Document Type Not Found");

            var newUser = await _userService.getUserById(document_SubmittedDTO.Approved_by);
            if(newUser == null)
            return NotFound("User Not Found");
            
            try{
                var response = await _document_SubmittedService.saveDocument_Submitted(document_SubmittedDTO);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{Document_Submitted_id}")]
        public async Task<ActionResult<Document_Submitted>> updateDocument_Submitted(int Document_Submitted_id,Document_SubmittedDTO document_SubmittedDTO)
        {
            var newCandidate = await _candidate_DetailsService.getCandidate_DetailsById(document_SubmittedDTO.Candidate_id);
            if(newCandidate == null)
            return NotFound("Candidate Not Found");
            
            var newJob = await _jobsSerivce.getJobById(document_SubmittedDTO.Job_id);
            if(newJob == null)
            return NotFound("Job Not Found");

            var newDocument_Type = await  _document_TypeService.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
            if(newDocument_Type == null)
            return NotFound("Document Type Not Found");

            var newUser = await _userService.getUserById(document_SubmittedDTO.Approved_by);
            if(newUser == null)
            return NotFound("User Not Found");

            try{
                var response = await _document_SubmittedService.updateDocument_Submitted(Document_Submitted_id,document_SubmittedDTO);
                if(response == null)
                return NotFound("Submitted Document Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{Document_Submitted_id}")]
        public async Task<ActionResult<Document_Submitted>> deleteDocument_Submitted(int Document_Submitted_id)
        {
            try{
                var response = await _document_SubmittedService.deleteDocument_Submitted(Document_Submitted_id);
                if(response)
                return Ok("Submitted Document Deleted Successfully");
                else
                return NotFound("Submitted Document Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}