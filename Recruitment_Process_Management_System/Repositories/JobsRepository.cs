using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Recruitment_Process_Management_System.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly ApplicationDbContext _context;

        public JobsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteJob(int Job_id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Job_id == Job_id);
            if(job == null)
            return false;

            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Jobs> getAllJobs()
        {
            var jobs = _context.Jobs.Include(js => js.job_Status).Include(u => u.user).ToList();
            return jobs;
        }

        public Jobs getJobById(int Job_id)
        {
            var job = _context.Jobs.Include(js => js.job_Status).Include(u => u.user).FirstOrDefault(j => j.Job_id == Job_id);
            return job;
        }

        public Jobs saveJob(JobsDTO jobsDTO)
        {
            var responseUser = _context.Users.FirstOrDefault(u => u.User_id == jobsDTO.Created_by);
            var jobStatus = _context.Job_Status.FirstOrDefault(js => js.Job_Status_id == jobsDTO.Job_Status_id);

            if(responseUser == null || jobStatus == null)
            return null;

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

            _context.Jobs.Add(jobs);
            _context.SaveChanges();
            return jobs;
        }   

        public Jobs updateJob(int Job_id, JobsDTO jobsDTO)
        {
            var existingJobs = _context.Jobs.FirstOrDefault(j => j.Job_id == Job_id);
            if(existingJobs == null)
            return null;
            var responseUser = _context.Users.FirstOrDefault(u => u.User_id == jobsDTO.Created_by);
            var jobStatus = _context.Job_Status.FirstOrDefault(js => js.Job_Status_id == jobsDTO.Job_Status_id);

            if(responseUser == null || jobStatus == null)
            return null;
            existingJobs.Job_title = jobsDTO.Job_title;
            existingJobs.Job_description = jobsDTO.Job_description;
            existingJobs.Job_Status_id = jobsDTO.Job_Status_id;
            existingJobs.job_Status = jobStatus;
            existingJobs.Job_Closed_reason = jobsDTO.Job_Closed_reason;
            existingJobs.Job_Selected_Candidate_id = jobsDTO.Job_Selected_Candidate_id;
            existingJobs.Created_by = jobsDTO.Created_by;
            existingJobs.user = responseUser;
            existingJobs.Updated_at = DateTime.Now;
            return existingJobs;
        }
    }
}