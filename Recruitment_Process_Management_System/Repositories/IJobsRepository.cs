using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public interface IJobsRepository
    {
        Task<IEnumerable<Jobs>> getAllJobs();
        Task<Jobs> getJobById(int Job_id);
        Task<Jobs> saveJob(Jobs jobs);
        Task<Jobs> updateJob(Jobs jobs);
        Task<bool> deleteJob(Jobs jobs);
    }
}