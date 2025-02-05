using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Services
{
    public interface IJobsSerivce
    {
        IEnumerable<Jobs> getAllJobs();
        Jobs getJobById(int Job_id);
        Jobs saveJob(JobsDTO jobsDTO);
        Jobs updateJob(int Job_id,JobsDTO jobsDTO);
        bool deleteJob(int Job_id);
    }
}