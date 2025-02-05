using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IRequired_Job_SkillRepository
    {
        IEnumerable<Required_Job_Skill> getAllRequired_Job_Skill();
        Required_Job_Skill getRequired_Job_SkillById(int Required_Job_Skill_id);
        Required_Job_Skill saveRequired_Job_Skill(Required_Job_SkillDTO required_Job_SkillDTO);
        Required_Job_Skill updateRequired_Job_Skill(int Required_Job_Skill_id,Required_Job_SkillDTO required_Job_SkillDTO);
        bool deleteRequired_Job_Skill(int Required_Job_Skill_id);
    }
}