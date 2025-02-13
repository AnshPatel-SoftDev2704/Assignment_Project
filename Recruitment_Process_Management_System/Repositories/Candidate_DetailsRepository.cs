using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Candidate_DetailsRepository : ICandidate_DetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public Candidate_DetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteCandidate_Details(Candidate_Details candidate_Details)
        {
            _context.Candidate_Details.Remove(candidate_Details);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Candidate_Details>> getAllCandidate_Details()
        {
            var candidates = _context.Candidate_Details.Include(r => r.role).ToList();
            return candidates;
        }

        public async Task<Candidate_Details> getCandidate_DetailsById(int Candidate_id)
        {
            var candidate = _context.Candidate_Details.Include(r => r.role).FirstOrDefault(cd => cd.Candidate_id == Candidate_id);
            return candidate;
        }

        public async Task<Candidate_Details> saveCandidate_Details(Candidate_Details candidate_Details)
        {
            var candidate = _context.Candidate_Details.FirstOrDefault(cd => cd.Candidate_name == candidate_Details.Candidate_name);
            if(candidate != null)
            throw new Exception("Candidate Name is already Taken");
            _context.Candidate_Details.Add(candidate_Details);
            _context.SaveChanges();
            return candidate_Details;
        }

        public async Task<Candidate_Details> updateCandidate_Details(Candidate_Details candidate_Details)
        {
            _context.Candidate_Details.Update(candidate_Details);
            _context.SaveChanges();
            return candidate_Details;
        }

        public async Task<Candidate_Details> GetCandidate_DetailsByName(string name)
        {
            Candidate_Details response = _context.Candidate_Details.FirstOrDefault(cd => cd.Candidate_name == name);
            return response;
        }
    }
}