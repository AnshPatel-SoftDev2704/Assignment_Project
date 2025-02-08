using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Preferred_Job_SkillRepository : IPreferred_Job_SkillRepository
    {
        private readonly ApplicationDbContext _context;

        public Preferred_Job_SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deletePreferred_Job_Skill(Preferred_Job_Skill preferred_Job_Skill)
        {
            _context.Preferred_Job_Skill.Remove(preferred_Job_Skill);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Preferred_Job_Skill>> getAllPreferred_Job_Skill()
        {
            var preferred_Job_Skills = _context.Preferred_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).ToList();
            return preferred_Job_Skills;
        }

        public async Task<Preferred_Job_Skill> getPreferred_Job_SkillById(int Preferred_Job_Skill_id)
        {
            var preferred_Job_Skill = _context.Preferred_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).FirstOrDefault(pjs => pjs.Preferred_Job_Skill_id == Preferred_Job_Skill_id);
            return preferred_Job_Skill;
        }

        public async Task<Preferred_Job_Skill> savePreferred_Job_Skill(Preferred_Job_Skill preferred_Job_Skill)
        {
            _context.Preferred_Job_Skill.Add(preferred_Job_Skill);
            _context.SaveChanges();
            return preferred_Job_Skill;
        }

        public async Task<Preferred_Job_Skill> updatePreferred_Job_Skill(Preferred_Job_Skill preferred_Job_Skill)
        {
            _context.Preferred_Job_Skill.Update(preferred_Job_Skill);
            _context.SaveChanges();
            return preferred_Job_Skill;
        }
    }
}