using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Candidate_SkillsRepostiroy : ICandidate_SkillsRepostiroy
    {
        private readonly ApplicationDbContext _context;

        public Candidate_SkillsRepostiroy(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteCandidate_Skill(int Candidate_Skills_id)
        {
            var candidateSkill = _context.Candidate_Skills.FirstOrDefault(cs => cs.Candidate_Skill_id == Candidate_Skills_id);
            if(candidateSkill == null)
            return false;

            _context.Candidate_Skills.Remove(candidateSkill);
            _context.SaveChanges();
            return true;
        }

        public Candidate_Skills getAllCandidate_SkillById(int Candidate_Skills_id)
        {
           var candidateSkill = _context.Candidate_Skills.Include(c=> c.candidate_Details).Include(s => s.skill).Include(c => c.candidate_Details.role).FirstOrDefault(cs => cs.Candidate_Skill_id == Candidate_Skills_id);
           return candidateSkill;
        }

        public IEnumerable<Candidate_Skills> getAllCandidate_Skills()
        {
            var candidateSkills = _context.Candidate_Skills.Include(c => c.candidate_Details).Include(s => s.skill).Include(c => c.candidate_Details.role).ToList();
            return candidateSkills;
        }

        public Candidate_Skills saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO)
        {
            var candidateSkill = _context.Candidate_Skills.FirstOrDefault(c => c.Skill_id == candidate_SkillsDTO.Skill_id);
            if(candidateSkill != null)
            return null;
            
            var candidate = _context.Candidate_Details.FirstOrDefault(cd => cd.Candidate_id == candidate_SkillsDTO.Candidate_id);
            if(candidate == null)
            return null;

            var newSkill = _context.Skills.FirstOrDefault(s => s.Skill_id == candidate_SkillsDTO.Skill_id);
            if(newSkill == null)
            return null;

            Candidate_Skills candidate_Skills = new Candidate_Skills{
                Candidate_id = candidate_SkillsDTO.Candidate_id,
                candidate_Details = candidate,
                Skill_id = candidate_SkillsDTO.Skill_id,
                skill = newSkill,
                Total_Skill_Work_experience = candidate_SkillsDTO.Total_Skill_Work_experience,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            _context.Candidate_Skills.Add(candidate_Skills);
            _context.SaveChanges();
            return candidate_Skills;
        }

        public Candidate_Skills updateCandidate_Skill(int Candidate_Skills_id, Candidate_SkillsDTO candidate_SkillsDTO)
        {
           var candidateSkill = _context.Candidate_Skills.FirstOrDefault(c => c.Candidate_Skill_id == Candidate_Skills_id);
            if(candidateSkill == null)
            return null;
            
            var candidate = _context.Candidate_Details.FirstOrDefault(cd => cd.Candidate_id == candidate_SkillsDTO.Candidate_id);
            if(candidate == null)
            return null;

            var newSkill = _context.Skills.FirstOrDefault(s => s.Skill_id == candidate_SkillsDTO.Skill_id);
            if(newSkill == null)
            return null;

            candidateSkill.Candidate_id = candidate_SkillsDTO.Candidate_id;
            candidateSkill.candidate_Details = candidate;
            candidateSkill.Skill_id = candidate_SkillsDTO.Skill_id;
            candidateSkill.skill = newSkill;
            candidateSkill.Total_Skill_Work_experience = candidate_SkillsDTO.Total_Skill_Work_experience;
            candidateSkill.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return candidateSkill;
        }
    }
}