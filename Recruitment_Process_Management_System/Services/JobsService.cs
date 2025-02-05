using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Controllers;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class JobsService : IJobsSerivce
    {
        private readonly IJobsRepository _jobsRepository;

        public JobsService(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        public bool deleteJob(int Job_id) => _jobsRepository.deleteJob(Job_id);

        public IEnumerable<Jobs> getAllJobs() => _jobsRepository.getAllJobs();

        public Jobs getJobById(int Job_id) => _jobsRepository.getJobById(Job_id);
        public Jobs saveJob(JobsDTO jobsDTO) => _jobsRepository.saveJob(jobsDTO);

        public Jobs updateJob(int Job_id, JobsDTO jobsDTO) => _jobsRepository.updateJob(Job_id,jobsDTO);
    }
}