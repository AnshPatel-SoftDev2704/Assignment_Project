using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Required_Job_SkillRepository : IRequired_Job_SkillRepository
    {
        private readonly ApplicationDbContext _context;

        public Required_Job_SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteRequired_Job_Skill(int Required_Job_Skill_id)
        {
            var required_Job_Skill = _context.Required_Job_Skill.FirstOrDefault(rjs => rjs.Required_Job_Skill_id == Required_Job_Skill_id);
            if(required_Job_Skill == null)
            return false;

            _context.Required_Job_Skill.Remove(required_Job_Skill);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Required_Job_Skill> getAllRequired_Job_Skill()
        {
            var required_Job_Skills = _context.Required_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).ToList();
            return required_Job_Skills;
        }

        public Required_Job_Skill getRequired_Job_SkillById(int Required_Job_Skill_id)
        {
            var required_Job_Skills = _context.Required_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).FirstOrDefault(rjs => rjs.Required_Job_Skill_id == Required_Job_Skill_id);
            return required_Job_Skills;
        }

        public Required_Job_Skill saveRequired_Job_Skill(Required_Job_SkillDTO required_Job_SkillDTO)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Job_id == required_Job_SkillDTO.Job_id);
            var responseSkill = _context.Skills.FirstOrDefault(s => s.Skill_id == required_Job_SkillDTO.Skill_id);

            if(job == null || responseSkill == null)
            return null;

            Required_Job_Skill required_Job_Skill = new Required_Job_Skill{
                Job_id = required_Job_SkillDTO.Job_id,
                jobs = job,
                Skill_id = required_Job_SkillDTO.Skill_id,
                skill = responseSkill,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            _context.Required_Job_Skill.Add(required_Job_Skill);
            _context.SaveChanges();
            return required_Job_Skill;
        }

        public Required_Job_Skill updateRequired_Job_Skill(int Required_Job_Skill_id, Required_Job_SkillDTO required_Job_SkillDTO)
        {
            var existingRequired_Job_Skill = _context.Required_Job_Skill.First(rjs => rjs.Required_Job_Skill_id == Required_Job_Skill_id);
            if(existingRequired_Job_Skill == null)
            return null;
            var job = _context.Jobs.FirstOrDefault(j => j.Job_id == required_Job_SkillDTO.Job_id);
            var responseSkill = _context.Skills.FirstOrDefault(s => s.Skill_id == required_Job_SkillDTO.Skill_id);

            if(job == null || responseSkill == null)
            return null;

            existingRequired_Job_Skill.Job_id = required_Job_SkillDTO.Job_id;
            existingRequired_Job_Skill.jobs = job;
            existingRequired_Job_Skill.Skill_id = required_Job_SkillDTO.Skill_id;
            existingRequired_Job_Skill.skill = responseSkill;
            existingRequired_Job_Skill.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingRequired_Job_Skill;
        }
    }
}