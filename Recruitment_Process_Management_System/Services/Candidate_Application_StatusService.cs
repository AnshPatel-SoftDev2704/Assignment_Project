using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Candidate_Application_StatusService : ICandidate_Application_StatusService
    {
        private readonly ICandidate_Application_StatusRepository _candidate_Application_StatusRepository;
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;
        private readonly IJobsRepository _jobsRepository;
        private readonly IApplication_StatusRepository _application_StatusRepository;
        private readonly INotification_CandidateService _notification_CandidateService;

        public Candidate_Application_StatusService(ICandidate_Application_StatusRepository candidate_Application_StatusRepository,ICandidate_DetailsRepository candidate_DetailsRepository,
        IJobsRepository jobsRepository,IApplication_StatusRepository application_StatusRepository,
        INotification_CandidateService notification_CandidateService)
        {
            _candidate_Application_StatusRepository = candidate_Application_StatusRepository;
            _candidate_DetailsRepository = candidate_DetailsRepository;
            _jobsRepository = jobsRepository;
            _application_StatusRepository = application_StatusRepository;
            _notification_CandidateService = notification_CandidateService;
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
            await _notification_CandidateService.saveCandidateNotification(notifications_CandidateDTO);
        }
    }
}