using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IApplication_StatusService
    {
        Task<IEnumerable<Application_Status>> getAllApplication_Status();

        Task<Application_Status> getApplication_StatusById(int Application_Status_id);

        Task<Application_Status> saveApplication_Status(Application_Status application_Status);

        Task<Application_Status> updateApplication_Status(int Application_Status_id,Application_Status application_Status);

        Task<bool> deleteApplication_Status(int Application_Status_id);
    }
}