using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Services
{
    public interface IJobStatusService
    {
        Task<IEnumerable<Job_Status>> getAllJobStatus();

        Task<Job_Status> getJobStatusById(int Job_Status_id);

        Task<Job_Status> saveJobStatus(Job_Status jobStatus);

        Task<Job_Status> updateJobStatus(int Job_Status_id,Job_Status jobStatus);

        Task<bool> deleteJobStatus(int Job_Status_id);
    }
}