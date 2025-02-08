using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> getAllSkill();
        Task<Skill> getSkillById(int Skill_id);
        Task<Skill> saveSkill(Skill skill);
        Task<Skill> updateSkill(Skill skill);
        Task<bool> deleteSkill(Skill skill);
    }
}