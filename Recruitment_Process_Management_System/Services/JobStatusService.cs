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

        public bool deleteJobStatus(int Job_Status_id) => _jobStatusRepository.deleteJobStatus(Job_Status_id);

        public IEnumerable<Job_Status> getAllJobStatus() => _jobStatusRepository.getAllJobStatus();

        public Job_Status getJobStatusById(int Job_Status_id) => _jobStatusRepository.GetJobStatusById(Job_Status_id);

        public Job_Status saveJobStatus(Job_Status jobStatus) =>  _jobStatusRepository.saveJobStatus(jobStatus);

        public Job_Status updateJobStatus(int Job_Status_id, Job_Status jobStatus) => _jobStatusRepository.updateJobStatus(Job_Status_id,jobStatus);
    }
}