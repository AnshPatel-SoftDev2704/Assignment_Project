using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Candidate_DetailsService : ICandidate_DetailsService
    {
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IApplication_StatusRepository _application_StatusRepository;
        private readonly ICandidate_SkillsRepostiroy _candidate_SkillsRepostiroy;
        private readonly ISkillRepository _skillRepository;
        private readonly ICandidate_Application_StatusRepository _candidate_Application_StatusRepository;
        private readonly IJobsRepository _jobsRepository;
        private readonly INotifications_UsersService _notifications_UsersService;
        
        public Candidate_DetailsService(ICandidate_DetailsRepository candidate_DetailsRepository,IRoleRepository roleRepository,IApplication_StatusRepository application_StatusRepository,ICandidate_SkillsRepostiroy candidate_SkillsRepostiroy,
        ISkillRepository skillRepository,ICandidate_Application_StatusRepository candidate_Application_StatusRepository
        ,IJobsRepository jobsRepository,INotifications_UsersService notifications_UsersService)
        {
            _candidate_DetailsRepository = candidate_DetailsRepository;
            _roleRepository = roleRepository;
            _application_StatusRepository = application_StatusRepository;
            _candidate_SkillsRepostiroy = candidate_SkillsRepostiroy;
            _skillRepository = skillRepository;
            _jobsRepository = jobsRepository;
            _candidate_Application_StatusRepository = candidate_Application_StatusRepository;
            _notifications_UsersService = notifications_UsersService;
        }

        public async Task<bool> deleteCandidate_Details(int Candidate_id) {
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(Candidate_id);
            if(candidate == null)
            return false;
            return await _candidate_DetailsRepository.deleteCandidate_Details(candidate);
        }

        public async Task<IEnumerable<Candidate_Details>> getAllCandidate_Details() => await _candidate_DetailsRepository.getAllCandidate_Details();

        public async Task<Candidate_Details> getCandidate_DetailsById(int Candidate_id) => await _candidate_DetailsRepository.getCandidate_DetailsById(Candidate_id);

        public async Task<Candidate_Details> saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO) {
            var responseRole = await _roleRepository.getRoleById(candidate_DetailsDTO.Role_id);
            if(responseRole == null)
            throw new Exception("Role Not Found");

            Candidate_Details newCandidate = new Candidate_Details{
                Candidate_name = candidate_DetailsDTO.Candidate_name,
                Candidate_DOB = candidate_DetailsDTO.Candidate_DOB,
                Candidate_email = candidate_DetailsDTO.Candidate_email,
                Candidate_password = candidate_DetailsDTO.Candidate_password,
                Candidate_Total_Work_Experience = candidate_DetailsDTO.Candidate_Total_Work_Experience,
                PhoneNo = candidate_DetailsDTO.PhoneNo,
                CV_Path = candidate_DetailsDTO.CV_Path,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Role_id = candidate_DetailsDTO.Role_id,
                role = responseRole
            };
            return await _candidate_DetailsRepository.saveCandidate_Details(newCandidate);
        }

        public async Task<Candidate_Details> updateCandidate_Details(int Candidate_id, Candidate_DetailsDTO candidate_DetailsDTO) {
            var existingCandidate = await _candidate_DetailsRepository.getCandidate_DetailsById(Candidate_id);
            if(existingCandidate == null)
            throw new Exception("Candidate Not Found");

            var responseRole = await _roleRepository.getRoleById(candidate_DetailsDTO.Role_id);
            if(responseRole == null)
            throw new Exception("Role Not Found");

            existingCandidate.Candidate_name = candidate_DetailsDTO.Candidate_name;
            existingCandidate.Candidate_DOB = candidate_DetailsDTO.Candidate_DOB;
            existingCandidate.Candidate_email = candidate_DetailsDTO.Candidate_email;
            existingCandidate.Candidate_password = candidate_DetailsDTO.Candidate_password;
            existingCandidate.Candidate_Total_Work_Experience = candidate_DetailsDTO.Candidate_Total_Work_Experience;
            existingCandidate.PhoneNo = candidate_DetailsDTO.PhoneNo;
            existingCandidate.CV_Path = candidate_DetailsDTO.CV_Path;
            existingCandidate.Updated_at = DateTime.Now;
            existingCandidate.Role_id = candidate_DetailsDTO.Role_id;
            existingCandidate.role = responseRole;
            return await _candidate_DetailsRepository.updateCandidate_Details(existingCandidate);
        }
        public async Task<IEnumerable<Application_Status>> getAllApplication_Status() => await _application_StatusRepository.getAllApplication_Status();

        public async Task<Application_Status> getApplication_StatusById(int Application_Status_id) => await _application_StatusRepository.getApplication_StatusById(Application_Status_id);

        public async Task<Application_Status> saveApplication_Status(Application_Status application_Status) {
            return await _application_StatusRepository.saveApplication_Status(application_Status);
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

        public async Task<bool> deleteCandidate_Application_Status(int Candidate_Application_Status_id) {
            var response = await _candidate_Application_StatusRepository.getCandidate_Application_StatusById(Candidate_Application_Status_id);
            if(response == null)
            return false;
            return await _candidate_Application_StatusRepository.deleteCandidate_Application_Status(response);
        }

        public async Task<IEnumerable<Candidate_Application_Status>> getAllCandidate_Application_Status() => await _candidate_Application_StatusRepository.getAllCandidate_Application_Status();

        public async Task<Candidate_Application_Status> getCandidate_Application_StatusById(int Candidate_Application_Status_id) => await _candidate_Application_StatusRepository.getCandidate_Application_StatusById(Candidate_Application_Status_id);

        public async Task<Candidate_Application_Status> saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO) {
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(candidate_Application_StatusDTO.Candidate_id);
            if(candidate == null)
            throw new Exception("Candidate Not Found");

            var newJob = await _jobsRepository.getJobById(candidate_Application_StatusDTO.Job_id);
            if(newJob == null)
            throw new Exception("Job Not Found");

            var applicationStatus = await _application_StatusRepository.getApplication_StatusById(candidate_Application_StatusDTO.Application_Status_id);
            if(applicationStatus == null)
            throw new Exception("Application Status Not Found");

            Candidate_Application_Status candidate_Application_Status = new Candidate_Application_Status{
                Candidate_id = candidate_Application_StatusDTO.Candidate_id,
                candidate_Details = candidate,
                Job_id = candidate_Application_StatusDTO.Job_id,
                job = newJob,
                Application_Status_id = candidate_Application_StatusDTO.Application_Status_id,
                application_Status = applicationStatus,
                Applied_Date = candidate_Application_StatusDTO.Applied_Date,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _candidate_Application_StatusRepository.saveCandidate_Application_Status(candidate_Application_Status);
        }

        public async Task<Candidate_Application_Status> updateCandidate_Application_Status(int Candidate_Application_Status_id, Candidate_Application_StatusDTO candidate_Application_StatusDTO) {
            var response = await _candidate_Application_StatusRepository.getCandidate_Application_StatusById(Candidate_Application_Status_id);
            if(response == null)
            throw new Exception("Candidate Application Not Found");
            
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(candidate_Application_StatusDTO.Candidate_id);
            if(candidate == null)
            throw new Exception("Candidate Not Found");

            var newJob = await _jobsRepository.getJobById(candidate_Application_StatusDTO.Job_id);
            if(newJob == null)
            throw new Exception("Job Not Found");

            var applicationStatus = await _application_StatusRepository.getApplication_StatusById(candidate_Application_StatusDTO.Application_Status_id);
            if(applicationStatus == null)
            throw new Exception("Application Not Found");


            response.Candidate_id = candidate_Application_StatusDTO.Candidate_id;
            response.candidate_Details = candidate;
            response.Job_id = candidate_Application_StatusDTO.Job_id;
            response.job = newJob;
            response.Application_Status_id = candidate_Application_StatusDTO.Application_Status_id;
            response.application_Status = applicationStatus;
            response.Applied_Date = candidate_Application_StatusDTO.Applied_Date;
            response.Updated_at = DateTime.Now;
            Console.WriteLine(response.application_Status.Application_Status_Name != applicationStatus.Application_Status_Name);
            if(!(response.application_Status.Application_Status_Name != applicationStatus.Application_Status_Name))
            {
                await sendNotification(response);
            }
            return await _candidate_Application_StatusRepository.updateCandidate_Application_Status(response);
        }

        public async Task sendNotification(Candidate_Application_Status candidate_Application_Status)
        {
            string message = "Your Application Status for Job title " + candidate_Application_Status.job.Job_title + " is " + candidate_Application_Status.application_Status.Application_Status_Name;
            Notifications_CandidateDTO notifications_CandidateDTO = new Notifications_CandidateDTO
            {
                Candidate_id  = candidate_Application_Status.Candidate_id,
                Message = message,
                Status = false
            };
            await _notifications_UsersService.saveCandidateNotification(notifications_CandidateDTO);
        }

        public async Task<Candidate_Details> GetCandidate_DetailsByName(string Name)
        {
            Candidate_Details candidate = await _candidate_DetailsRepository.GetCandidate_DetailsByName(Name);
            return candidate;
        }
    }
}