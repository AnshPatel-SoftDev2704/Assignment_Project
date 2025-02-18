using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,HR,Candidate")]
    public class Document_SubmittedController : ControllerBase
    {
        private readonly IDocument_SubmittedService? _document_SubmittedService;
        private readonly ICandidate_DetailsService _candidate_DetailsService;
        private readonly IJobsSerivce _jobsSerivce;
        private readonly IUserService _userService;

        public Document_SubmittedController(IDocument_SubmittedService document_SubmittedService,ICandidate_DetailsService candidate_DetailsService,IJobsSerivce jobsSerivce,IUserService userService)
        {
            _document_SubmittedService = document_SubmittedService;
            _candidate_DetailsService = candidate_DetailsService;
            _jobsSerivce = jobsSerivce;
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

        [HttpGet("GetFile")]
        public ActionResult GetFile([FromQuery] string filepath)
        {
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

        [HttpPost]
        [Authorize(Roles = "Admin,HR,Candidate")]
        public async Task<ActionResult<Document_Submitted>> saveDocument_Submitted([FromForm] Document_SubmittedDTO document_SubmittedDTO,IFormFile file)
        {
            var newCandidate = await  _candidate_DetailsService.getCandidate_DetailsById(document_SubmittedDTO.Candidate_id);
            if(newCandidate == null)
            return NotFound("Candidate Not Found");
            
            var newJob = await _jobsSerivce.getJobById(document_SubmittedDTO.Job_id);
            if(newJob == null)
            return NotFound("Job Not Found");

            var newDocument_Type = await _document_SubmittedService.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
            if(newDocument_Type == null)
            return NotFound("Document Type Not Found");
            
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\uploads");
            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(uploadPath);
            }
            var filePath = Path.Combine(uploadPath, file.FileName);
            using (var target = new FileStream(filePath, FileMode.Create)) {
                file.CopyTo(target);
            }

            document_SubmittedDTO.Document_path = filePath;

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

            var newDocument_Type = await  _document_SubmittedService.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
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
        

        [HttpPut("UpdateStatus/{Document_Submitted_id}/{Approved_by}")]
        public async Task<ActionResult<Document_Submitted>> updateStatus(int Document_Submitted_id,[FromBody] bool Status,int Approved_by)
        {
            try
            {
                var response = await _document_SubmittedService.updateStatus(Document_Submitted_id,Status,Approved_by);
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

        [HttpGet("GetAllDocumentType")]
        [Authorize(Roles = "Admin,HR,Candidate")]
        public async Task<ActionResult<IEnumerable<Document_Type>>> getAllDocument_Type()
        {
            try{
                var responses = await _document_SubmittedService.getAllDocument_Type();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDocumentTypeById/{Document_Type_id}")]
        public async Task<ActionResult<Document_Type>> getDocument_TypeById(int Document_Type_id)
        {
            try{
                var response = await _document_SubmittedService.getDocument_TypeById(Document_Type_id);
                if(response == null)
                return NotFound("Document Type Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("SaveDocumentType")]
        public async Task<ActionResult<Document_Type>> saveDocument_Type(Document_Type document_Type)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _document_SubmittedService.saveDocumenty_Type(document_Type);
                if(response == null)
                return BadRequest("Document Type is already Present");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("UpdateDocumentType/{Document_Type_id}")]
        public async Task<ActionResult<Document_Type>> updateDocument_Type(int Document_Type_id,Document_Type document_Type)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _document_SubmittedService.updateDocument_Type(Document_Type_id,document_Type);
                if(response == null)
                return NotFound("Document Type Not Found");
                else
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("DeleteDocumentType/{Document_Type_id}")]
        public async Task<ActionResult<Document_Type>> deleteDocument_Type(int Document_Type_id)
        {
             try{
                var response = await _document_SubmittedService.deleteDocument_Type(Document_Type_id);
                if(response)
                return Ok("Document Type Deleted Successfully");
                else
                return NotFound("Document Type Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}