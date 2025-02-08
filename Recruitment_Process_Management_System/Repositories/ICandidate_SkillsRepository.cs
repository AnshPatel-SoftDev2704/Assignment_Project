using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface ICandidate_SkillsRepostiroy
    {
        Task<IEnumerable<Candidate_Skills>> getAllCandidate_Skills();

        Task<Candidate_Skills> getAllCandidate_SkillById(int Candidate_Skills_id);

        Task<Candidate_Skills> saveCandidate_Skill(Candidate_Skills candidate_Skills);

        Task<Candidate_Skills> updateCandidate_Skill(Candidate_Skills candidate_Skills);

        Task<bool> deleteCandidate_Skill(Candidate_Skills candidate_Skills);
    }
}