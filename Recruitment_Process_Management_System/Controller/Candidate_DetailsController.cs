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
        public async Task<ActionResult<Candidate_Details>> updateCandidate_Details(int Candidate_id,[FromForm] Candidate_DetailsDTO candidate_DetailsDTO,IFormFile file)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/uploads");
            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(uploadPath);
            }
            var filePath = Path.Combine(uploadPath, file.FileName);
            using (var target = new FileStream(filePath, FileMode.Create)) {
                file.CopyTo(target);
            }
            candidate_DetailsDTO.CV_Path = filePath;
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
        [HttpGet("GetFile")]
        public ActionResult GetFile([FromQuery] string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return BadRequest("File path is required.");
            }

            var filePath = Path.Combine("YourFileDirectory", filepath);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var mimeType = "application/pdf";
            return PhysicalFile(filePath, mimeType);
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

        [HttpGet("GetAllApplicationStatus")]
        public async Task<ActionResult<IEnumerable<Application_Status>>> getAllApplication_Status()
        {
            try{
                var applicationStatuses = await _candidate_DetailsService.getAllApplication_Status();
                return Ok(applicationStatuses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetApplicationStatusById/{Application_Status_id}")]
        public async Task<ActionResult<Application_Status>> getApplication_StatusById(int Application_Status_id)
        {
            try{
                var applicationStatus = await _candidate_DetailsService.getApplication_StatusById(Application_Status_id);
                if(applicationStatus == null)
                return NotFound("Application Status Not Found");
                else
                return Ok(applicationStatus);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("SaveApplicationStatus")]
        public async Task<ActionResult<Application_Status>> saveApplication_Status(Application_Status application_Status)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var applicationStatus = await _candidate_DetailsService.saveApplication_Status(application_Status);
                if(applicationStatus == null)
                return BadRequest("Something went wrong");
                else
                return Ok(applicationStatus);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("GetAllCandidateSkills")]
        public async Task<ActionResult<IEnumerable<Candidate_Skills>>> getAllCandidate_Skills()
        {
            try{
                var candidateSkills = await _candidate_DetailsService.getAllCandidate_Skills();
                return Ok(candidateSkills);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCandidateSkillById/{Candidate_Skill_id}")]
        public async Task<ActionResult<Candidate_Skills>> getAllCandidate_SkillById(int Candidate_Skill_id)
        {
            try{
                var candidateSkill = await _candidate_DetailsService.getAllCandidate_SkillById(Candidate_Skill_id);
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

        [HttpPost("SaveCandidateSkill")]
        public async Task<ActionResult<Candidate_Skills>> saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var candidateSkill = await _candidate_DetailsService.saveCandidate_Skill(candidate_SkillsDTO);
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

        [HttpPut("UpdateCandiateSkill/{Candidate_Skill_id}")]
        public async Task<ActionResult<Candidate_Skills>> updateCandidate_Skill(int Candidate_Skill_id,Candidate_SkillsDTO candidate_SkillsDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var candidateSkill = await _candidate_DetailsService.updateCandidate_Skill(Candidate_Skill_id,candidate_SkillsDTO);
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

        [HttpDelete("DeleteCandidateSkill/{Candidate_Skill_id}")]
        public async Task<ActionResult<Candidate_Skills>> deleteCandidate_Skill(int Candidate_Skill_id)
        {
            try{
                var response = await _candidate_DetailsService.deleteCandidate_Skill(Candidate_Skill_id);
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

        [HttpGet("GetAllCandidateApplicationStatus")]
        public async Task<ActionResult<IEnumerable<Candidate_Application_Status>>> getAllCandidate_Application_Status()
        {
            try{
                var response = await _candidate_DetailsService.getAllCandidate_Application_Status();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCandidateApplicationStatusById/{Candidate_Application_Status_id}")]
        public async Task<ActionResult<Candidate_Application_Status>> getCandidate_Application_StatusById(int Candidate_Application_Status_id)
        {
            try{
                var response = await _candidate_DetailsService.getCandidate_Application_StatusById(Candidate_Application_Status_id);
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

        [HttpPost("SaveCandidateApplicationStatus")]
        public async Task<ActionResult<Candidate_Application_Status>> saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            Console.WriteLine(candidate_Application_StatusDTO.Candidate_id);
            try{
                var response = await _candidate_DetailsService.saveCandidate_Application_Status(candidate_Application_StatusDTO);
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

        [HttpPut("UpdateCandidateApplicationStatus/{Candidate_Application_Status_id}")]
        public async Task<ActionResult<Candidate_Application_Status>> updateCandidate_Application_Status(int Candidate_Application_Status_id,Candidate_Application_StatusDTO candidate_Application_StatusDTO)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _candidate_DetailsService.updateCandidate_Application_Status(Candidate_Application_Status_id,candidate_Application_StatusDTO);
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

        [HttpDelete("DeleteCandidateApplicationStatus/{Candidate_Application_Status_id}")]
        public async Task<ActionResult<Candidate_Application_Status>> deleteCandidate_Application_Status(int Candidate_Application_Status_id)
        {
            try{
                var response = await _candidate_DetailsService.deleteCandidate_Application_Status(Candidate_Application_Status_id);
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

        [HttpGet("getCandidateByName/{Name}")]
        public async Task<ActionResult<Candidate_Details>> getCandidate_DetailsByName(string Name)
        {
            try{
                var response = await _candidate_DetailsService.GetCandidate_DetailsByName(Name);
                return response;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}