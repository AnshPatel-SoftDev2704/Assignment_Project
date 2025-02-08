using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IJobStatusRepository
    {
        Task<IEnumerable<Job_Status>> getAllJobStatus();

        Task<Job_Status> GetJobStatusById(int Job_Status_id);
        Task<Job_Status> saveJobStatus(Job_Status jobStatus);
        Task<Job_Status> updateJobStatus(Job_Status jobStatus);
        Task<bool> deleteJobStatus(Job_Status jobStatus);
    }
}