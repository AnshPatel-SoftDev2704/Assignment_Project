using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Controllers;
using Recruitment_Process_Management_System.Repositories;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Services
{
    public class JobsService : IJobsSerivce
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobStatusRepository _jobStatusRepository;
        private readonly IRequired_Job_SkillRepository _required_Job_SkillRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IPreferred_Job_SkillRepository _preferred_Job_SkillRepository;

        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;


        public JobsService(IJobsRepository jobsRepository,IUserRepository userRepository,
        IJobStatusRepository jobStatusRepository,IRequired_Job_SkillRepository required_Job_SkillRepository
        ,ISkillRepository skillRepository,IPreferred_Job_SkillRepository preferred_Job_SkillRepository,
        ICandidate_DetailsRepository candidate_DetailsRepository)
        {
            _jobsRepository = jobsRepository;
            _userRepository = userRepository;
            _jobStatusRepository = jobStatusRepository;
            _required_Job_SkillRepository = required_Job_SkillRepository;
            _skillRepository = skillRepository;
            _preferred_Job_SkillRepository = preferred_Job_SkillRepository;
            _candidate_DetailsRepository = candidate_DetailsRepository;
        }

        public async Task<bool> deleteJob(int Job_id) {
            var job = await _jobsRepository.getJobById(Job_id);
            if(job == null)
            return false;
            return await _jobsRepository.deleteJob(job);
        }

        public async Task<IEnumerable<Jobs>> getAllJobs() => await _jobsRepository.getAllJobs();

        public async Task<Jobs> getJobById(int Job_id) => await _jobsRepository.getJobById(Job_id);
        public async Task<Jobs> saveJob(JobsDTO jobsDTO) {
            var responseUser = await _userRepository.getUserById(jobsDTO.Created_by);
            var jobStatus = await _jobStatusRepository.GetJobStatusById(jobsDTO.Job_Status_id);

            if(responseUser == null)
            throw new Exception("User Not Found");

            if(jobStatus == null)
            throw new Exception("Job Status Not Found");

            Jobs jobs = new Jobs{
                Job_title = jobsDTO.Job_title,
                Job_description = jobsDTO.Job_description,
                Job_Status_id = jobsDTO.Job_Status_id,
                job_Status = jobStatus,
                Job_Closed_reason = jobsDTO.Job_Closed_reason,
                Job_Selected_Candidate_id = jobsDTO.Job_Selected_Candidate_id,
                Created_by = jobsDTO.Created_by,
                user = responseUser,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
           return await _jobsRepository.saveJob(jobs);
        }

        public async Task<Jobs> updateJob(int Job_id, JobsDTO jobsDTO) {
            var existingJobs = await _jobsRepository.getJobById(Job_id);
            if(existingJobs == null)
            throw new Exception("Job Not Found");

            var responseUser = await _userRepository.getUserById(jobsDTO.Created_by);
            var jobStatus = await _jobStatusRepository.GetJobStatusById(jobsDTO.Job_Status_id);
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(jobsDTO.Job_Selected_Candidate_id);

            if(candidate == null)
            throw new Exception("Selected Candidate Not Found");

            if(responseUser == null)
            throw new Exception("User Not Found");

            if(jobStatus == null)
            throw new Exception("Job Status Not Found");

            if(jobsDTO.Job_Status_id == 4)
            {
                Console.WriteLine(jobsDTO.Job_Status_id);
                UserDTO user = new UserDTO{
                name = candidate.Candidate_name,
                email = candidate.Candidate_email,
                contact = candidate.PhoneNo.ToString(),
                password = candidate.Candidate_password
                };
                await _userRepository.saveUser(user);
                // await _candidate_DetailsRepository.deleteCandidate_Details(candidate);
            }

            existingJobs.Job_title = jobsDTO.Job_title;
            existingJobs.Job_description = jobsDTO.Job_description;
            existingJobs.Job_Status_id = jobsDTO.Job_Status_id;
            existingJobs.job_Status = jobStatus;
            existingJobs.Job_Closed_reason = jobsDTO.Job_Closed_reason;
            existingJobs.Job_Selected_Candidate_id = jobsDTO.Job_Selected_Candidate_id;
            existingJobs.Created_by = jobsDTO.Created_by;
            existingJobs.user = responseUser;
            existingJobs.Updated_at = DateTime.Now;
            return await _jobsRepository.updateJob(existingJobs);
        } 

         public async Task<IEnumerable<Job_Status>> getAllJobStatus() 
        {
            return await _jobStatusRepository.getAllJobStatus();
        }
        public async Task<Job_Status> getJobStatusById(int Job_Status_id) {
           return await _jobStatusRepository.GetJobStatusById(Job_Status_id);
        }

        public async Task<Job_Status> saveJobStatus(Job_Status jobStatus) {
            Job_Status job_Status = new Job_Status{
                Job_Status_name = jobStatus.Job_Status_name,
                Job_Status_Description = jobStatus.Job_Status_Description,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _jobStatusRepository.saveJobStatus(job_Status);
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