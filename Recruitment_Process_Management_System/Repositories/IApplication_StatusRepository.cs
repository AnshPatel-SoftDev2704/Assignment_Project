using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IApplication_StatusRepository
    {
        Task<IEnumerable<Application_Status>> getAllApplication_Status();

        Task<Application_Status> getApplication_StatusById(int Application_Status_id);

        Task<Application_Status> saveApplication_Status(Application_Status application_Status);

        Task<Application_Status> updateApplication_Status(Application_Status application_Status);

        Task<bool> deleteApplication_Status(Application_Status application_Status);
    }
}