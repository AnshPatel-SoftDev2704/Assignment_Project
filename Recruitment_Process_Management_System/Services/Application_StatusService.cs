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

        public bool deleteApplication_Status(int Application_Status_id) => _application_StatusRepository.deleteApplication_Status(Application_Status_id);

        public IEnumerable<Application_Status> getAllApplication_Status() => _application_StatusRepository.getAllApplication_Status();

        public Application_Status getApplication_StatusById(int Application_Status_id) => _application_StatusRepository.getApplication_StatusById(Application_Status_id);

        public Application_Status saveApplication_Status(Application_Status application_Status) => _application_StatusRepository.saveApplication_Status(application_Status);

        public Application_Status updateApplication_Status(int Application_Status_id, Application_Status application_Status) => _application_StatusRepository.updateApplication_Status(Application_Status_id,application_Status);

    }
}