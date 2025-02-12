using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Services
{
    public interface IJobsSerivce
    {
        Task<IEnumerable<Jobs>> getAllJobs();
        Task<Jobs> getJobById(int Job_id);
        Task<Jobs> saveJob(JobsDTO jobsDTO);
        Task<Jobs> updateJob(int Job_id,JobsDTO jobsDTO);
        Task<bool> deleteJob(int Job_id);
        Task<IEnumerable<Job_Status>> getAllJobStatus();

        Task<Job_Status> getJobStatusById(int Job_Status_id);

        Task<Job_Status> saveJobStatus(Job_Status jobStatus);

        Task<IEnumerable<Required_Job_Skill>> getAllRequired_Job_Skill();
        Task<Required_Job_Skill> getRequired_Job_SkillById(int Required_Job_Skill_id);
        Task<Required_Job_Skill> saveRequired_Job_Skill(Required_Job_SkillDTO required_Job_SkillDTO);
        Task<Required_Job_Skill> updateRequired_Job_Skill(int Required_Job_Skill_id,Required_Job_SkillDTO required_Job_SkillDTO);
        Task<bool> deleteRequired_Job_Skill(int Required_Job_Skill_id);
        Task<IEnumerable<Preferred_Job_Skill>> getAllPreferred_Job_Skill();
        Task<Preferred_Job_Skill> getPreferred_Job_SkillById(int Preferred_Job_Skill_id);
        Task<Preferred_Job_Skill> savePreferred_Job_Skill(Preferred_Job_SkillDTO preferred_Job_SkillDTO);
        Task<Preferred_Job_Skill> updatePreferred_Job_Skill(int Preferred_Job_Skill_id,Preferred_Job_SkillDTO preferred_Job_SkillDTO);
        Task<bool> deletePreferred_Job_Skill(int Preferred_Job_Skill_id);
    }
}