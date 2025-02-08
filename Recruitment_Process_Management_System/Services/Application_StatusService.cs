using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Application_StatusService : IApplication_StatusService
    {
        private readonly IApplication_StatusRepository _application_StatusRepository;

        public Application_StatusService(IApplication_StatusRepository application_StatusRepository)
        {
            _application_StatusRepository = application_StatusRepository;
        }

        public async Task<bool> deleteApplication_Status(int Application_Status_id) {
            var applicationStatus = await _application_StatusRepository.getApplication_StatusById(Application_Status_id);
            if(applicationStatus == null)
            return false;
            return await _application_StatusRepository.deleteApplication_Status(applicationStatus);
        }

        public async Task<IEnumerable<Application_Status>> getAllApplication_Status() => await _application_StatusRepository.getAllApplication_Status();

        public async Task<Application_Status> getApplication_StatusById(int Application_Status_id) => await _application_StatusRepository.getApplication_StatusById(Application_Status_id);

        public async Task<Application_Status> saveApplication_Status(Application_Status application_Status) {
            return await _application_StatusRepository.saveApplication_Status(application_Status);
        }

        public async Task<Application_Status> updateApplication_Status(int Application_Status_id, Application_Status application_Status) {
            var existingApplicationStatus = await _application_StatusRepository.getApplication_StatusById(Application_Status_id);
            if(application_Status == null)
            throw new Exception("Application Station Not Found");

            existingApplicationStatus.Application_Status_Name = application_Status.Application_Status_Name;
            existingApplicationStatus.Application_Status_Description = application_Status.Application_Status_Description;
            existingApplicationStatus.Updated_at = DateTime.Now;
            return await _application_StatusRepository.updateApplication_Status(existingApplicationStatus);
        } 

    }
}