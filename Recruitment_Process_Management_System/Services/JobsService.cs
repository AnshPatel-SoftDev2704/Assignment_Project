using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Controllers;
using Recruitment_Process_Management_System.Repositories;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Services
{
    public class JobsService : IJobsSerivce
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobStatusRepository _jobStatusRepository;

        public JobsService(IJobsRepository jobsRepository,IUserRepository userRepository,
        IJobStatusRepository jobStatusRepository)
        {
            _jobsRepository = jobsRepository;
            _userRepository = userRepository;
            _jobStatusRepository = jobStatusRepository;
        }

        public async Task<bool> deleteJob(int Job_id) {
            var job = await _jobsRepository.getJobById(Job_id);
            if(job == null)
            return false;
            return await _jobsRepository.deleteJob(job);
        }

        public async Task<IEnumerable<Jobs>> getAllJobs() => await _jobsRepository.getAllJobs();

        public async Task<Jobs> getJobById(int Job_id) => await _jobsRepository.getJobById(Job_id);
        public async Task<Jobs> saveJob(JobsDTO jobsDTO) {
            var responseUser = await _userRepository.getUserById(jobsDTO.Created_by);
            var jobStatus = await _jobStatusRepository.GetJobStatusById(jobsDTO.Job_Status_id);

            if(responseUser == null)
            throw new Exception("User Not Found");

            if(jobStatus == null)
            throw new Exception("Job Status Not Found");

            Jobs jobs = new Jobs{
                Job_title = jobsDTO.Job_title,
                Job_description = jobsDTO.Job_description,
                Job_Status_id = jobsDTO.Job_Status_id,
                job_Status = jobStatus,
                Job_Closed_reason = jobsDTO.Job_Closed_reason,
                Job_Selected_Candidate_id = jobsDTO.Job_Selected_Candidate_id,
                Created_by = jobsDTO.Created_by,
                user = responseUser,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
           return await _jobsRepository.saveJob(jobs);
        }

        public async Task<Jobs> updateJob(int Job_id, JobsDTO jobsDTO) {
            var existingJobs = await _jobsRepository.getJobById(Job_id);
            if(existingJobs == null)
            throw new Exception("Job Not Found");

            var responseUser = await _userRepository.getUserById(jobsDTO.Created_by);
            var jobStatus = await _jobStatusRepository.GetJobStatusById(jobsDTO.Job_Status_id);

            if(responseUser == null)
            throw new Exception("User Not Found");

            if(jobStatus == null)
            throw new Exception("Job Status Not Found");

            existingJobs.Job_title = jobsDTO.Job_title;
            existingJobs.Job_description = jobsDTO.Job_description;
            existingJobs.Job_Status_id = jobsDTO.Job_Status_id;
            existingJobs.job_Status = jobStatus;
            existingJobs.Job_Closed_reason = jobsDTO.Job_Closed_reason;
            existingJobs.Job_Selected_Candidate_id = jobsDTO.Job_Selected_Candidate_id;
            existingJobs.Created_by = jobsDTO.Created_by;
            existingJobs.user = responseUser;
            existingJobs.Updated_at = DateTime.Now;
            return await _jobsRepository.updateJob(existingJobs);
        } 
    }
}