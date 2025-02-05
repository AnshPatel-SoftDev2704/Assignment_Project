using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Required_Job_SkillService : IRequired_Job_SkillService
    {
        private readonly IRequired_Job_SkillRepository _required_Job_SkillRepository;

        public Required_Job_SkillService(IRequired_Job_SkillRepository required_Job_SkillRepository)
        {
            _required_Job_SkillRepository = required_Job_SkillRepository;
        }

        public bool deleteRequired_Job_Skill(int Required_Job_Skill_id) => _required_Job_SkillRepository.deleteRequired_Job_Skill(Required_Job_Skill_id);

        public IEnumerable<Required_Job_Skill> getAllRequired_Job_Skill() => _required_Job_SkillRepository.getAllRequired_Job_Skill();

        public Required_Job_Skill getRequired_Job_SkillById(int Required_Job_Skill_id) => _required_Job_SkillRepository.getRequired_Job_SkillById(Required_Job_Skill_id);

        public Required_Job_Skill saveRequired_Job_Skill(Required_Job_SkillDTO required_Job_SkillDTO) => _required_Job_SkillRepository.saveRequired_Job_Skill(required_Job_SkillDTO);

        public Required_Job_Skill updateRequired_Job_Skill(int Required_Job_Skill_id, Required_Job_SkillDTO required_Job_SkillDTO) => _required_Job_SkillRepository.updateRequired_Job_Skill(Required_Job_Skill_id,required_Job_SkillDTO);
    }
}