using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;
namespace Recruitment_Process_Management_System.Services
{
    public class Preferred_Job_SkillService : IPreferred_Job_SkillService
    {
        private readonly IPreferred_Job_SkillRepository _preferred_Job_SkillRepository;

        public Preferred_Job_SkillService(IPreferred_Job_SkillRepository preferred_Job_SkillRepository)
        {
            _preferred_Job_SkillRepository = preferred_Job_SkillRepository;
        }

        public bool deletePreferred_Job_Skill(int Preferred_Job_Skill_id) => _preferred_Job_SkillRepository.deletePreferred_Job_Skill(Preferred_Job_Skill_id);

        public IEnumerable<Preferred_Job_Skill> getAllPreferred_Job_Skill() => _preferred_Job_SkillRepository.getAllPreferred_Job_Skill();

        public Preferred_Job_Skill getPreferred_Job_SkillById(int Preferred_Job_Skill_id) => _preferred_Job_SkillRepository.getPreferred_Job_SkillById(Preferred_Job_Skill_id);

        public Preferred_Job_Skill savePreferred_Job_Skill(Preferred_Job_SkillDTO preferred_Job_SkillDTO) => _preferred_Job_SkillRepository.savePreferred_Job_Skill(preferred_Job_SkillDTO);
        public Preferred_Job_Skill updatePreferred_Job_Skill(int Preferred_Job_Skill_id, Preferred_Job_SkillDTO preferred_Job_SkillDTO) => _preferred_Job_SkillRepository.updatePreferred_Job_Skill(Preferred_Job_Skill_id,preferred_Job_SkillDTO);
    }
}