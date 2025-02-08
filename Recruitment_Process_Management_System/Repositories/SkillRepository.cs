using System.Threading.Tasks;
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
        public async Task<bool> deleteSkill(Skill skill)
        {
            _context.Skills.Remove(skill);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Skill>> getAllSkill()
        {
           var skills = _context.Skills.ToList();
           return skills;
        }

        public async Task<Skill> getSkillById(int Skill_id)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.Skill_id == Skill_id);
            return skill;
        }

        public async Task<Skill> saveSkill(Skill skill)
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

        public async Task<Skill> updateSkill(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
            return skill;
        }
    }
}