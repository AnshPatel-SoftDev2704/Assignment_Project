using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public interface ISkillRepository
    {
        IEnumerable<Skill> getAllSkill();
        Skill getSkillById(int Skill_id);
        Skill saveSkill(Skill skill);
        Skill updateSkill(int Skill_id,Skill skill);
        bool deleteSkill(int Skill_id);
    }
}