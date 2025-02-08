using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Repositories
{
    [ApiController]
    [Route("api/[controller]")]
    public class Document_TypeController : ControllerBase
    {
        private readonly IDocument_TypeService _document_TypeService;

        public Document_TypeController(IDocument_TypeService document_TypeService)
        {
            _document_TypeService = document_TypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document_Type>>> getAllDocument_Type()
        {
            try{
                var responses = await _document_TypeService.getAllDocument_Type();
                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Document_Type_id}")]
        public async Task<ActionResult<Document_Type>> getDocument_TypeById(int Document_Type_id)
        {
            try{
                var response = await _document_TypeService.getDocument_TypeById(Document_Type_id);
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

        [HttpPost]
        public async Task<ActionResult<Document_Type>> saveDocument_Type(Document_Type document_Type)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _document_TypeService.saveDocumenty_Type(document_Type);
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

        [HttpPut("{Document_Type_id}")]
        public async Task<ActionResult<Document_Type>> updateDocument_Type(int Document_Type_id,Document_Type document_Type)
        {
            if(!this.ModelState.IsValid)
            return BadRequest(ModelState);
            try{
                var response = await _document_TypeService.updateDocument_Type(Document_Type_id,document_Type);
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

        [HttpDelete("{Document_Type_id}")]
        public async Task<ActionResult<Document_Type>> deleteDocument_Type(int Document_Type_id)
        {
             try{
                var response = await _document_TypeService.deleteDocument_Type(Document_Type_id);
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