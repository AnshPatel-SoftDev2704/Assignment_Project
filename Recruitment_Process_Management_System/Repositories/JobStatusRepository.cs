using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class JobStatusRepository : IJobStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public JobStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteJobStatus(Job_Status jobStatus)
        {
            _context.Job_Status.Remove(jobStatus);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Job_Status>> getAllJobStatus()
        {
            return _context.Job_Status.ToList();
        }

        public async Task<Job_Status> GetJobStatusById(int Job_Status_id)
        {
            var existingJobStatus = _context.Job_Status.FirstOrDefault(js => js.Job_Status_id == Job_Status_id);
            return existingJobStatus;
        }

        public async Task<Job_Status> saveJobStatus(Job_Status jobStatus)
        {
            var existingJobStatus = _context.Job_Status.FirstOrDefault(js => js.Job_Status_name == jobStatus.Job_Status_name);
            if(existingJobStatus != null)
            return null;
            _context.Job_Status.Add(jobStatus);
            _context.SaveChanges();
            return jobStatus;
        }

        public async Task<Job_Status> updateJobStatus(Job_Status jobStatus)
        {
            _context.Job_Status.Update(jobStatus);
            _context.SaveChanges();
            return jobStatus;
        }
    }
}