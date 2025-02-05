using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IJobStatusRepository
    {
        IEnumerable<Job_Status> getAllJobStatus();

        Job_Status GetJobStatusById(int Job_Status_id);
        Job_Status saveJobStatus(Job_Status jobStatus);
        Job_Status updateJobStatus(int Job_Status_id, Job_Status jobStatus);
        bool deleteJobStatus(int Job_Status_id);
    }
}