using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Required_Job_SkillRepository : IRequired_Job_SkillRepository
    {
        private readonly ApplicationDbContext _context;

        public Required_Job_SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteRequired_Job_Skill(Required_Job_Skill required_Job_Skill)
        {
            _context.Required_Job_Skill.Remove(required_Job_Skill);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Required_Job_Skill>> getAllRequired_Job_Skill()
        {
            var required_Job_Skills = _context.Required_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).ToList();
            return required_Job_Skills;
        }

        public async Task<Required_Job_Skill> getRequired_Job_SkillById(int Required_Job_Skill_id)
        {
            var required_Job_Skills = _context.Required_Job_Skill.Include(j => j.jobs).Include(s => s.skill).Include(js => js.jobs.job_Status).Include(js => js.jobs.user).FirstOrDefault(rjs => rjs.Required_Job_Skill_id == Required_Job_Skill_id);
            return required_Job_Skills;
        }

        public async Task<Required_Job_Skill> saveRequired_Job_Skill(Required_Job_Skill required_Job_Skill)
        {
            _context.Required_Job_Skill.Add(required_Job_Skill);
            _context.SaveChanges();
            return required_Job_Skill;
        }

        public async Task<Required_Job_Skill> updateRequired_Job_Skill(Required_Job_Skill required_Job_Skill)
        {
            _context.Required_Job_Skill.Update(required_Job_Skill);
            _context.SaveChanges();
            return required_Job_Skill;
        }
    }
}