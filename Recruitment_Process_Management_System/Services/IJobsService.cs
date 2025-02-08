using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Services
{
    public interface IJobsSerivce
    {
        Task<IEnumerable<Jobs>> getAllJobs();
        Task<Jobs> getJobById(int Job_id);
        Task<Jobs> saveJob(JobsDTO jobsDTO);
        Task<Jobs> updateJob(int Job_id,JobsDTO jobsDTO);
        Task<bool> deleteJob(int Job_id);
    }
}