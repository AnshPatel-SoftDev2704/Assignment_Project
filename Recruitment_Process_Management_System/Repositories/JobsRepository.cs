using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly ApplicationDbContext _context;

        public JobsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteJob(Jobs job)
        {
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Jobs>> getAllJobs()
        {
            var jobs = _context.Jobs.Include(js => js.job_Status).Include(u => u.user).ToList();
            return jobs;
        }

        public async Task<Jobs> getJobById(int Job_id)
        {
            var job = _context.Jobs.Include(js => js.job_Status).Include(u => u.user).FirstOrDefault(j => j.Job_id == Job_id);
            return job;
        }

        public async Task<Jobs> saveJob(Jobs jobs)
        {
            _context.Jobs.Add(jobs);
            _context.SaveChanges();
            return jobs;
        }   

        public async Task<Jobs> updateJob(Jobs jobs)
        {
            _context.Jobs.Update(jobs);
            _context.SaveChanges();
            return jobs;
        }
    }
}