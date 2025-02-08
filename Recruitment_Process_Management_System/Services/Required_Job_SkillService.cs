using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Required_Job_SkillService : IRequired_Job_SkillService
    {
        private readonly IRequired_Job_SkillRepository _required_Job_SkillRepository;
        private readonly IJobsRepository _jobsRepository;
        private readonly ISkillRepository _skillRepository;

        public Required_Job_SkillService(IRequired_Job_SkillRepository required_Job_SkillRepository,IJobsRepository jobsRepository,ISkillRepository skillRepository)
        {
            _required_Job_SkillRepository = required_Job_SkillRepository;
            _jobsRepository = jobsRepository;
            _skillRepository = skillRepository;
        }

        public async Task<bool> deleteRequired_Job_Skill(int Required_Job_Skill_id) {
            var required_Job_Skill = await _required_Job_SkillRepository.getRequired_Job_SkillById(Required_Job_Skill_id);
            if(required_Job_Skill == null)
            return false;

            return await _required_Job_SkillRepository.deleteRequired_Job_Skill(required_Job_Skill);
        }

        public async Task<IEnumerable<Required_Job_Skill>> getAllRequired_Job_Skill() => await _required_Job_SkillRepository.getAllRequired_Job_Skill();

        public async Task<Required_Job_Skill> getRequired_Job_SkillById(int Required_Job_Skill_id) => await _required_Job_SkillRepository.getRequired_Job_SkillById(Required_Job_Skill_id);

        public async Task<Required_Job_Skill> saveRequired_Job_Skill(Required_Job_SkillDTO required_Job_SkillDTO) {
            var job = await _jobsRepository.getJobById(required_Job_SkillDTO.Job_id);
            var responseSkill = await _skillRepository.getSkillById(required_Job_SkillDTO.Skill_id);

            if(job == null)
            throw new Exception("Job Not found");

            if(responseSkill == null)
            throw new Exception("Skill Not Found");

            Required_Job_Skill required_Job_Skill = new Required_Job_Skill{
                Job_id = required_Job_SkillDTO.Job_id,
                jobs = job,
                Skill_id = required_Job_SkillDTO.Skill_id,
                skill = responseSkill,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
           return await _required_Job_SkillRepository.saveRequired_Job_Skill(required_Job_Skill);
        }

        public async Task<Required_Job_Skill> updateRequired_Job_Skill(int Required_Job_Skill_id, Required_Job_SkillDTO required_Job_SkillDTO) {
            var existingRequired_Job_Skill = await _required_Job_SkillRepository.getRequired_Job_SkillById(Required_Job_Skill_id);
            if(existingRequired_Job_Skill == null)
            throw new Exception("Required Job Skill Not Found");

            var job = await _jobsRepository.getJobById(required_Job_SkillDTO.Job_id);
            var responseSkill = await _skillRepository.getSkillById(required_Job_SkillDTO.Skill_id);

            if(job == null)
            throw new Exception("Job Not found");

            if(responseSkill == null)
            throw new Exception("Skill Not Found");

            existingRequired_Job_Skill.Job_id = required_Job_SkillDTO.Job_id;
            existingRequired_Job_Skill.jobs = job;
            existingRequired_Job_Skill.Skill_id = required_Job_SkillDTO.Skill_id;
            existingRequired_Job_Skill.skill = responseSkill;
            existingRequired_Job_Skill.Updated_at = DateTime.Now;

            return await _required_Job_SkillRepository.updateRequired_Job_Skill(existingRequired_Job_Skill);
        }
    }
}