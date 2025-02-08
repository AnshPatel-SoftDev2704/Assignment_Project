using System.Threading.Tasks;
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
        public async Task<bool> deleteSkill(int Skill_id) {
            var skill = await _skillRepository.getSkillById(Skill_id);
            if(skill == null)
            throw new Exception("Skill Not Found");
            return await _skillRepository.deleteSkill(skill);
        }

        public async Task<IEnumerable<Skill>> getAllSkill() {
            return await _skillRepository.getAllSkill();
        }

        public async Task<Skill> getSkillById(int Skill_id) {
           return await  _skillRepository.getSkillById(Skill_id);
        }

        public async Task<Skill> saveSkill(Skill skill) {
           return await  _skillRepository.saveSkill(skill);
        }

        public async Task<Skill> updateSkill(int Skill_id, Skill skill) {
            var existingSkill = await _skillRepository.getSkillById(Skill_id);
            if(existingSkill == null)
            return null;

            existingSkill.Skill_name = skill.Skill_name;
            existingSkill.Skill_description = skill.Skill_description;
            existingSkill.Updated_at = DateTime.Now;
            return await _skillRepository.updateSkill(existingSkill);
        }
    }
}