using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public bool deleteSkill(int Skill_id) => _skillRepository.deleteSkill(Skill_id);

        public IEnumerable<Skill> getAllSkill() => _skillRepository.getAllSkill();

        public Skill getSkillById(int Skill_id) => _skillRepository.getSkillById(Skill_id);

        public Skill saveSkill(Skill skill) => _skillRepository.saveSkill(skill);

        public Skill updateSkill(int Skill_id, Skill skill) => _skillRepository.updateSkill(Skill_id,skill);
    }
}