using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IPreferred_Job_SkillRepository
    {
        IEnumerable<Preferred_Job_Skill> getAllPreferred_Job_Skill();
        Preferred_Job_Skill getPreferred_Job_SkillById(int Preferred_Job_Skill_id);
        Preferred_Job_Skill savePreferred_Job_Skill(Preferred_Job_SkillDTO preferred_Job_SkillDTO);
        Preferred_Job_Skill updatePreferred_Job_Skill(int Preferred_Job_Skill_id,Preferred_Job_SkillDTO preferred_Job_SkillDTO);
        bool deletePreferred_Job_Skill(int Preferred_Job_Skill_id);
    }
}