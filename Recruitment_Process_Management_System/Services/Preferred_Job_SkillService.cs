using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;
namespace Recruitment_Process_Management_System.Services
{
    public class Preferred_Job_SkillService : IPreferred_Job_SkillService
    {
        private readonly IPreferred_Job_SkillRepository _preferred_Job_SkillRepository;
        private readonly IJobsRepository _jobsRepository;
        private readonly ISkillRepository _skillRepository;

        public Preferred_Job_SkillService(IPreferred_Job_SkillRepository preferred_Job_SkillRepository,
        IJobsRepository jobsRepository,ISkillRepository skillRepository)
        {
            _preferred_Job_SkillRepository = preferred_Job_SkillRepository;
            _jobsRepository = jobsRepository;
            _skillRepository = skillRepository;
        }

        public async Task<bool> deletePreferred_Job_Skill(int Preferred_Job_Skill_id) 
        {
            var preferred_Job_Skill = await _preferred_Job_SkillRepository.getPreferred_Job_SkillById(Preferred_Job_Skill_id);
            if(preferred_Job_Skill == null)
            return false;

            return await _preferred_Job_SkillRepository.deletePreferred_Job_Skill(preferred_Job_Skill);
        }

        public async Task<IEnumerable<Preferred_Job_Skill>> getAllPreferred_Job_Skill() => await _preferred_Job_SkillRepository.getAllPreferred_Job_Skill();

        public async Task<Preferred_Job_Skill> getPreferred_Job_SkillById(int Preferred_Job_Skill_id) => await _preferred_Job_SkillRepository.getPreferred_Job_SkillById(Preferred_Job_Skill_id);

        public async Task<Preferred_Job_Skill> savePreferred_Job_Skill(Preferred_Job_SkillDTO preferred_Job_SkillDTO) 
        {
            var job = await _jobsRepository.getJobById(preferred_Job_SkillDTO.Job_id);
            var responseSkill = await _skillRepository.getSkillById(preferred_Job_SkillDTO.Skill_id);

            if(job == null)
            throw new Exception("Job Not found");

            if(responseSkill == null)
            throw new Exception("Skill Not Found");

            Preferred_Job_Skill preferred_Job_Skill = new Preferred_Job_Skill{
                Job_id = preferred_Job_SkillDTO.Job_id,
                jobs = job,
                Skill_id = preferred_Job_SkillDTO.Skill_id,
                skill = responseSkill,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _preferred_Job_SkillRepository.savePreferred_Job_Skill(preferred_Job_Skill);
        }

        public async Task<Preferred_Job_Skill> updatePreferred_Job_Skill(int Preferred_Job_Skill_id, Preferred_Job_SkillDTO preferred_Job_SkillDTO) {
            var existingPreferred_Job_Skill = await _preferred_Job_SkillRepository.getPreferred_Job_SkillById(Preferred_Job_Skill_id);
            if(existingPreferred_Job_Skill == null)
            throw new Exception("Preferred Skill Not Found");

            var job = await _jobsRepository.getJobById(preferred_Job_SkillDTO.Job_id);
            var responseSkill = await _skillRepository.getSkillById(preferred_Job_SkillDTO.Skill_id);

            if(job == null)
            throw new Exception("Job Not found");

            if(responseSkill == null)
            throw new Exception("Skill Not Found");

            existingPreferred_Job_Skill.Job_id = preferred_Job_SkillDTO.Job_id;
            existingPreferred_Job_Skill.jobs = job;
            existingPreferred_Job_Skill.Skill_id = preferred_Job_SkillDTO.Skill_id;
            existingPreferred_Job_Skill.skill = responseSkill;
            existingPreferred_Job_Skill.Updated_at = DateTime.Now;
            return await _preferred_Job_SkillRepository.updatePreferred_Job_Skill(existingPreferred_Job_Skill);
        }
    }
}