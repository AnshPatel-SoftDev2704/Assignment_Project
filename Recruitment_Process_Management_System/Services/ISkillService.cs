using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Services
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> getAllSkill();
        Task<Skill> getSkillById(int Skill_id);
        Task<Skill> saveSkill(Skill skill);
        Task<Skill> updateSkill(int Skill_id,Skill skill);
        Task<bool> deleteSkill(int Skill_id);
    }
}