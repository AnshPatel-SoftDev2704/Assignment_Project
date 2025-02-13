using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Candidate_SkillsRepostiroy : ICandidate_SkillsRepostiroy
    {
        private readonly ApplicationDbContext _context;

        public Candidate_SkillsRepostiroy(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteCandidate_Skill(Candidate_Skills candidate_Skills)
        {
            _context.Candidate_Skills.Remove(candidate_Skills);
            _context.SaveChanges();
            return true;
        }

        public async Task<Candidate_Skills> getAllCandidate_SkillById(int Candidate_Skills_id)
        {
           var candidateSkill = _context.Candidate_Skills.Include(c=> c.candidate_Details).Include(s => s.skill).Include(c => c.candidate_Details.role).FirstOrDefault(cs => cs.Candidate_Skill_id == Candidate_Skills_id);
           return candidateSkill;
        }

        public async Task<IEnumerable<Candidate_Skills>> getAllCandidate_Skills()
        {
            var candidateSkills = _context.Candidate_Skills.Include(c => c.candidate_Details).Include(s => s.skill).Include(c => c.candidate_Details.role).ToList();
            return candidateSkills;
        }

        public async Task<Candidate_Skills> saveCandidate_Skill(Candidate_Skills candidate_Skills )
        {
            _context.Candidate_Skills.Add(candidate_Skills);
            _context.SaveChanges();
            return candidate_Skills;
        }

        public async Task<Candidate_Skills> updateCandidate_Skill(Candidate_Skills candidate_Skills)
        {
            _context.Candidate_Skills.Update(candidate_Skills);
            _context.SaveChanges();
            return candidate_Skills;
        }
    }
}