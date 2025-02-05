using Recruitment_Process_Management_System.data;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;
        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool deleteSkill(int Skill_id)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.Skill_id == Skill_id);
            if(skill == null)
            return false;
            _context.Skills.Remove(skill);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Skill> getAllSkill()
        {
           var skills = _context.Skills.ToList();
           return skills;
        }

        public Skill getSkillById(int Skill_id)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.Skill_id == Skill_id);
            return skill;
        }

        public Skill saveSkill(Skill skill)
        {
            var isSkillExist = _context.Skills.FirstOrDefault(s => s.Skill_name == skill.Skill_name);
            if(isSkillExist != null)
            return null;
            Skill newSkill = new Skill{
                Skill_name = skill.Skill_name,
                Skill_description = skill.Skill_description,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            _context.Skills.Add(newSkill);
            _context.SaveChanges();
            return newSkill;
        }

        public Skill updateSkill(int Skill_id, Skill skill)
        {
            var existingSkill = _context.Skills.FirstOrDefault(s => s.Skill_id == Skill_id);
            if(existingSkill == null)
            return null;

            existingSkill.Skill_name = skill.Skill_name;
            existingSkill.Skill_description = skill.Skill_description;
            existingSkill.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingSkill;
        }
    }
}