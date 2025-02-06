using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Candidate_SkillService : ICandidate_SkillService
    {
        private readonly ICandidate_SkillsRepostiroy _candidate_SkillsRepostiroy;
        
        public Candidate_SkillService(ICandidate_SkillsRepostiroy candidate_SkillsRepostiroy)
        {
            _candidate_SkillsRepostiroy = candidate_SkillsRepostiroy;
        }

        public bool deleteCandidate_Skill(int Candidate_Skills_id) => _candidate_SkillsRepostiroy.deleteCandidate_Skill(Candidate_Skills_id);

        public Candidate_Skills getAllCandidate_SkillById(int Candidate_Skills_id) => _candidate_SkillsRepostiroy.getAllCandidate_SkillById(Candidate_Skills_id);

        public IEnumerable<Candidate_Skills> getAllCandidate_Skills() => _candidate_SkillsRepostiroy.getAllCandidate_Skills();
        
        public Candidate_Skills saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO) => _candidate_SkillsRepostiroy.saveCandidate_Skill(candidate_SkillsDTO);

        public Candidate_Skills updateCandidate_Skill(int Candidate_Skills_id, Candidate_SkillsDTO candidate_SkillsDTO) => _candidate_SkillsRepostiroy.updateCandidate_Skill(Candidate_Skills_id,candidate_SkillsDTO);
    }
}