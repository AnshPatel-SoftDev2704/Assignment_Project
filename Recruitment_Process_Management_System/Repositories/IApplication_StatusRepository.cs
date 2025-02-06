using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IApplication_StatusRepository
    {
        IEnumerable<Application_Status> getAllApplication_Status();

        Application_Status getApplication_StatusById(int Application_Status_id);

        Application_Status saveApplication_Status(Application_Status application_Status);

        Application_Status updateApplication_Status(int Application_Status_id,Application_Status application_Status);

        bool deleteApplication_Status(int Application_Status_id);
    }
}