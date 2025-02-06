using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface ICandidate_SkillsRepostiroy
    {
        IEnumerable<Candidate_Skills> getAllCandidate_Skills();

        Candidate_Skills getAllCandidate_SkillById(int Candidate_Skills_id);

        Candidate_Skills saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO);

        Candidate_Skills updateCandidate_Skill(int Candidate_Skills_id,Candidate_SkillsDTO candidate_SkillsDTO);

        bool deleteCandidate_Skill(int Candidate_Skills_id);
    }
}