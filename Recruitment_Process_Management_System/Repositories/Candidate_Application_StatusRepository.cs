using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Candidate_Application_StatusRepository : ICandidate_Application_StatusRepository
    {
        private readonly ApplicationDbContext _context;

        public Candidate_Application_StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteCandidate_Application_Status(Candidate_Application_Status candidate_Application_Status)
        {
            _context.Candidate_Application_Status.Remove(candidate_Application_Status);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Candidate_Application_Status>> getAllCandidate_Application_Status()
        {
            var response = _context.Candidate_Application_Status.Include(c => c.candidate_Details).Include(j => j.job).Include(j => j.job.job_Status).Include(j => j.job.user).Include(a => a.application_Status).ToList();
            return response;
        }

        public async Task<Candidate_Application_Status> getCandidate_Application_StatusById(int Candidate_Application_Status_id)
        {
            var response = _context.Candidate_Application_Status.Include(c => c.candidate_Details).Include(j => j.job).Include(j => j.job.job_Status).Include(j => j.job.user).Include(a => a.application_Status).FirstOrDefault(cas => cas.Candidate_Application_Status_id == Candidate_Application_Status_id);
            return response;
        }

        public async Task<Candidate_Application_Status> saveCandidate_Application_Status(Candidate_Application_Status candidate_Application_Status)
        {
            var response = _context.Candidate_Application_Status.FirstOrDefault(cas => cas.Candidate_id == candidate_Application_Status.Candidate_id);
            if(response != null)
            throw new Exception("Candidate Application is Already Present");

            _context.Candidate_Application_Status.Add(candidate_Application_Status);
            _context.SaveChanges();
            return candidate_Application_Status;
        }

        public async Task<Candidate_Application_Status> updateCandidate_Application_Status(Candidate_Application_Status candidate_Application_Status)
        {
            _context.Candidate_Application_Status.Update(candidate_Application_Status);
            _context.SaveChanges();
            return candidate_Application_Status;
        }
    }
}