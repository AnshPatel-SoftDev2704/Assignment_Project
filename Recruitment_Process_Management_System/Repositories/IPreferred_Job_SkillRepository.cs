using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IPreferred_Job_SkillRepository
    {
        Task<IEnumerable<Preferred_Job_Skill>> getAllPreferred_Job_Skill();
        Task<Preferred_Job_Skill> getPreferred_Job_SkillById(int Preferred_Job_Skill_id);
        Task<Preferred_Job_Skill> savePreferred_Job_Skill(Preferred_Job_Skill preferred_Job_Skill);
        Task<Preferred_Job_Skill> updatePreferred_Job_Skill(Preferred_Job_Skill preferred_Job_Skill);
        Task<bool> deletePreferred_Job_Skill(Preferred_Job_Skill preferred_Job_Skill);
    }
}