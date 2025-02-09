using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Selected_CandidateRepository : ISelected_CandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public Selected_CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Selected_Candidate>> getAllSelectedCandidate()
        {
            var responses = _context.Selected_Candidates.Include(sc => sc.candidate_Details).Include(sc => sc.candidate_Details.role).Include(sc => sc.job).Include(js => js.job.job_Status).Include(u => u.job.user).ToList();
            return responses; 
        }

        public async Task<Selected_Candidate> getSelecteCandidateById(int Selected_Candidate_id)
        {
            var response = _context.Selected_Candidates.Include(sc => sc.candidate_Details).Include(sc => sc.candidate_Details.role).Include(sc => sc.job).Include(js => js.job.job_Status).Include(u => u.job.user).FirstOrDefault(sc => sc.Selected_Candidate_id == Selected_Candidate_id);
            return response;
        }

        public async Task<Selected_Candidate> saveSelectedCadidate(Selected_Candidate selected_Candidate)
        {
            _context.Selected_Candidates.Add(selected_Candidate);
            _context.SaveChanges();
            return selected_Candidate;
        }

        public async Task<Selected_Candidate> updateSelectedCandidate(Selected_Candidate selected_Candidate)
        {
            _context.Selected_Candidates.Update(selected_Candidate);
            _context.SaveChanges();
            return selected_Candidate;
        }

        public async Task<bool> deleteSelectedCandidate(Selected_Candidate selected_Candidate)
        {
            _context.Selected_Candidates.Remove(selected_Candidate);
            _context.SaveChanges();
            return true;
        }

    }
}