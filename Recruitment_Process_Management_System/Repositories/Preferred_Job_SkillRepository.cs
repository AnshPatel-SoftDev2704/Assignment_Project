using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Preferred_Job_SkillRepository : IPreferred_Job_SkillRepository
    {
        private readonly ApplicationDbContext _context;

        public Preferred_Job_SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deletePreferred_Job_Skill(int Preferred_Job_Skill_id)
        {
            var preferred_Job_Skill = _context.Preferred_Job_Skill.FirstOrDefault(pjs => pjs.Preferred_Job_Skill_id == Preferred_Job_Skill_id);
            if(preferred_Job_Skill == null)
            return false;

            _context.Preferred_Job_Skill.Remove(preferred_Job_Skill);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Preferred_Job_Skill> getAllPreferred_Job_Skill()
        {
            var preferred_Job_Skills = _context.Preferred_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).ToList();
            return preferred_Job_Skills;
        }

        public Preferred_Job_Skill getPreferred_Job_SkillById(int Preferred_Job_Skill_id)
        {
            var preferred_Job_Skill = _context.Preferred_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).FirstOrDefault(pjs => pjs.Preferred_Job_Skill_id == Preferred_Job_Skill_id);
            return preferred_Job_Skill;
        }

        public Preferred_Job_Skill savePreferred_Job_Skill(Preferred_Job_SkillDTO preferred_Job_SkillDTO)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Job_id == preferred_Job_SkillDTO.Job_id);
            var responseSkill = _context.Skills.FirstOrDefault(s => s.Skill_id == preferred_Job_SkillDTO.Skill_id);

            if(job == null || responseSkill == null)
            return null;

            Preferred_Job_Skill preferred_Job_Skill = new Preferred_Job_Skill{
                Job_id = preferred_Job_SkillDTO.Job_id,
                jobs = job,
                Skill_id = preferred_Job_SkillDTO.Skill_id,
                skill = responseSkill,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            _context.Preferred_Job_Skill.Add(preferred_Job_Skill);
            _context.SaveChanges();
            return preferred_Job_Skill;
        }

        public Preferred_Job_Skill updatePreferred_Job_Skill(int Preferred_Job_Skill_id, Preferred_Job_SkillDTO preferred_Job_SkillDTO)
        {
            var existingPreferred_Job_Skill = _context.Preferred_Job_Skill.First(pjs => pjs.Preferred_Job_Skill_id == Preferred_Job_Skill_id);
            if(existingPreferred_Job_Skill == null)
            return null;
            var job = _context.Jobs.FirstOrDefault(j => j.Job_id == preferred_Job_SkillDTO.Job_id);
            var responseSkill = _context.Skills.FirstOrDefault(s => s.Skill_id == preferred_Job_SkillDTO.Skill_id);

            if(job == null || responseSkill == null)
            return null;

            existingPreferred_Job_Skill.Job_id = preferred_Job_SkillDTO.Job_id;
            existingPreferred_Job_Skill.jobs = job;
            existingPreferred_Job_Skill.Skill_id = preferred_Job_SkillDTO.Skill_id;
            existingPreferred_Job_Skill.skill = responseSkill;
            existingPreferred_Job_Skill.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingPreferred_Job_Skill;
        }
    }
}