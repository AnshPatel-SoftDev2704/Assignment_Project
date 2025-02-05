using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.VisualBasic;

namespace Recruitment_Process_Management_System.Repositories
{
    public class JobStatusRepository : IJobStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public JobStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteJobStatus(int Job_Status_id)
        {
            var isJobStatusExist = _context.Job_Status.FirstOrDefault(js => js.Job_Status_id == Job_Status_id);
            if(isJobStatusExist == null)
            return false;

            _context.Job_Status.Remove(isJobStatusExist);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Job_Status> getAllJobStatus()
        {
            return _context.Job_Status.ToList();
        }

        public Job_Status GetJobStatusById(int Job_Status_id)
        {
            var existingJobStatus = _context.Job_Status.FirstOrDefault(js => js.Job_Status_id == Job_Status_id);
            return existingJobStatus;
        }

        public Job_Status saveJobStatus(Job_Status jobStatus)
        {
            var existingJobStatus = _context.Job_Status.FirstOrDefault(js => js.Job_Status_name == jobStatus.Job_Status_name);
            if(existingJobStatus != null)
            return null;
            Job_Status job_Status = new Job_Status{
                Job_Status_name = jobStatus.Job_Status_name,
                Job_Status_Description = jobStatus.Job_Status_Description,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            _context.Job_Status.Add(job_Status);
            _context.SaveChanges();
            return jobStatus;
        }

        public Job_Status updateJobStatus(int Job_Status_id, Job_Status jobStatus)
        {
            var existingJobStatus = _context.Job_Status.FirstOrDefault(js => js.Job_Status_id == Job_Status_id);
            if(existingJobStatus == null)
            return null;

            existingJobStatus.Job_Status_name = jobStatus.Job_Status_name;
            existingJobStatus.Job_Status_Description = jobStatus.Job_Status_Description;
            existingJobStatus.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingJobStatus;
        }
    }
    
}