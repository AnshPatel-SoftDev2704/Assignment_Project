using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface ICandidate_SkillService
    {
        Task<IEnumerable<Candidate_Skills>> getAllCandidate_Skills();

        Task<Candidate_Skills> getAllCandidate_SkillById(int Candidate_Skills_id);

        Task<Candidate_Skills> saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO);

        Task<Candidate_Skills> updateCandidate_Skill(int Candidate_Skills_id,Candidate_SkillsDTO candidate_SkillsDTO);

        Task<bool> deleteCandidate_Skill(int Candidate_Skills_id);
    }
}