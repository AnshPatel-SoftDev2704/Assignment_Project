using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Candidate_SkillService : ICandidate_SkillService
    {
        private readonly ICandidate_SkillsRepostiroy _candidate_SkillsRepostiroy;
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;
        private readonly ISkillRepository _skillRepository;
        
        public Candidate_SkillService(ICandidate_SkillsRepostiroy candidate_SkillsRepostiroy,
        ICandidate_DetailsRepository candidate_DetailsRepository,
        ISkillRepository skillRepository)
        {
            _candidate_SkillsRepostiroy = candidate_SkillsRepostiroy;
            _candidate_DetailsRepository = candidate_DetailsRepository;
            _skillRepository = skillRepository;
        }

        public async Task<bool> deleteCandidate_Skill(int Candidate_Skills_id) {
            var candidateSkill = await _candidate_SkillsRepostiroy.getAllCandidate_SkillById(Candidate_Skills_id);
            if(candidateSkill == null)
            return false;
           return await _candidate_SkillsRepostiroy.deleteCandidate_Skill(candidateSkill);
        } 

        public async Task<Candidate_Skills> getAllCandidate_SkillById(int Candidate_Skills_id) => await _candidate_SkillsRepostiroy.getAllCandidate_SkillById(Candidate_Skills_id);

        public async Task<IEnumerable<Candidate_Skills>> getAllCandidate_Skills() => await _candidate_SkillsRepostiroy.getAllCandidate_Skills();
        
        public async Task<Candidate_Skills> saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO) {
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(candidate_SkillsDTO.Candidate_id);
            if(candidate == null)
            throw new Exception("Candidate Not Found");

            var newSkill = await _skillRepository.getSkillById(candidate_SkillsDTO.Skill_id);
            if(newSkill == null)
            throw new Exception("Skill Not Found");

            Candidate_Skills candidate_Skills = new Candidate_Skills{
                Candidate_id = candidate_SkillsDTO.Candidate_id,
                candidate_Details = candidate,
                Skill_id = candidate_SkillsDTO.Skill_id,
                skill = newSkill,
                Total_Skill_Work_experience = candidate_SkillsDTO.Total_Skill_Work_experience,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
           return await _candidate_SkillsRepostiroy.saveCandidate_Skill(candidate_Skills);
        }

        public async Task<Candidate_Skills> updateCandidate_Skill(int Candidate_Skills_id, Candidate_SkillsDTO candidate_SkillsDTO) {
            var candidateSkill = await _candidate_SkillsRepostiroy.getAllCandidate_SkillById(Candidate_Skills_id);
            if(candidateSkill == null)
            throw new Exception("Candidate Skill Not Found");
            
            var candidate = await  _candidate_DetailsRepository.getCandidate_DetailsById(candidate_SkillsDTO.Candidate_id);
            if(candidate == null)
            throw new Exception("Candidate Not Found");

            var newSkill = await _skillRepository.getSkillById(candidate_SkillsDTO.Skill_id);
            if(newSkill == null)
            throw new Exception("Skill Not Found");

            candidateSkill.Candidate_id = candidate_SkillsDTO.Candidate_id;
            candidateSkill.candidate_Details = candidate;
            candidateSkill.Skill_id = candidate_SkillsDTO.Skill_id;
            candidateSkill.skill = newSkill;
            candidateSkill.Total_Skill_Work_experience = candidate_SkillsDTO.Total_Skill_Work_experience;
            candidateSkill.Updated_at = DateTime.Now;
            return await _candidate_SkillsRepostiroy.updateCandidate_Skill(candidateSkill);
        }
    }
}