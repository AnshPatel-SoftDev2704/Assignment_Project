using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class JobStatusService : IJobStatusService
    {
        private readonly IJobStatusRepository _jobStatusRepository;

        public JobStatusService(IJobStatusRepository jobStatusRepository)
        {
            _jobStatusRepository = jobStatusRepository;
        }

        public async Task<bool> deleteJobStatus(int Job_Status_id) 
        {
            var isJobStatusExist = await _jobStatusRepository.GetJobStatusById(Job_Status_id);
            if(isJobStatusExist == null)
            return false;
            return await _jobStatusRepository.deleteJobStatus(isJobStatusExist);
        }

        public async Task<IEnumerable<Job_Status>> getAllJobStatus() 
        {
            return await _jobStatusRepository.getAllJobStatus();
        }
        public async Task<Job_Status> getJobStatusById(int Job_Status_id) {
           return await _jobStatusRepository.GetJobStatusById(Job_Status_id);
        }

        public async Task<Job_Status> saveJobStatus(Job_Status jobStatus) {
            Job_Status job_Status = new Job_Status{
                Job_Status_name = jobStatus.Job_Status_name,
                Job_Status_Description = jobStatus.Job_Status_Description,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _jobStatusRepository.saveJobStatus(job_Status);
        }

        public async Task<Job_Status> updateJobStatus(int Job_Status_id, Job_Status jobStatus) {
            var existingJobStatus = await _jobStatusRepository.GetJobStatusById(Job_Status_id);
            if(existingJobStatus == null)
            return null;

            existingJobStatus.Job_Status_name = jobStatus.Job_Status_name;
            existingJobStatus.Job_Status_Description = jobStatus.Job_Status_Description;
            existingJobStatus.Updated_at = DateTime.Now;
            return await _jobStatusRepository.updateJobStatus(existingJobStatus);
        }
    }
}