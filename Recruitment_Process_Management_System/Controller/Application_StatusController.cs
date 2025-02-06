using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Application_StatusController : ControllerBase
    {
        private readonly IApplication_StatusService _application_StatusService;

        public Application_StatusController(IApplication_StatusService application_StatusService) 
        {
            _application_StatusService = application_StatusService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Application_Status>> getAllApplication_Status()
        {
            var applicationStatuses = _application_StatusService.getAllApplication_Status();
            return Ok(applicationStatuses);
        }

        [HttpGet("{Application_Status_id}")]
        public ActionResult<Application_Status> getApplication_StatusById(int Application_Status_id)
        {
            var applicationStatus = _application_StatusService.getApplication_StatusById(Application_Status_id);
            if(applicationStatus == null)
            return NotFound("Application Status Not Found");
            else
            return Ok(applicationStatus);
        }

        [HttpPost]
        public ActionResult<Application_Status> saveApplication_Status(Application_Status application_Status)
        {
            var applicationStatus = _application_StatusService.saveApplication_Status(application_Status);
            if(applicationStatus == null)
            return BadRequest("Something went wrong");
            else
            return Ok(applicationStatus);
        }

        [HttpPut("{Application_Status_id}")]
        public ActionResult<Application_Status> updateApplication_Status(int Application_Status_id,Application_Status application_Status)
        {
            var applicationStatus = _application_StatusService.updateApplication_Status(Application_Status_id,application_Status);
            if(applicationStatus == null)
            return NotFound("Application Status Not Found");
            else
            return Ok(applicationStatus);
        }

        [HttpDelete("{Application_Status_id}")]
        public ActionResult<Application_Status> deleteApplication_Status(int Application_Status_id)
        {
            var response = _application_StatusService.deleteApplication_Status(Application_Status_id);
            if(response)
            return Ok("Application Status Deleted Successfully");
            else
            return NotFound("Application Status Not Found");
        }
    }
}