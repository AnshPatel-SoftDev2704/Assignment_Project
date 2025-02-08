using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly ICandidate_Application_StatusRepository _candidate_Application_StatusRepository;
        private readonly IInterview_TypeRepository _interview_TypeRepository;
        private readonly IInterview_StatusRepository _interview_StatusRepository;

        public InterviewService(IInterviewRepository interviewRepository,
        ICandidate_Application_StatusRepository candidate_Application_StatusRepository,
        IInterview_StatusRepository interview_StatusRepository,
        IInterview_TypeRepository interview_TypeRepository)
        {
            _interviewRepository = interviewRepository;
            _candidate_Application_StatusRepository = candidate_Application_StatusRepository;
            _interview_StatusRepository = interview_StatusRepository;
            _interview_TypeRepository = interview_TypeRepository;
        }

        public async Task<bool> deleteInterview(int Interview_id) {
            var response = await _interviewRepository.getInterviewById(Interview_id);
            if(response == null)
            return false;
            return await _interviewRepository.deleteInterview(response);
        }

        public async Task<IEnumerable<Interview>> getAllInterview() => await _interviewRepository.getAllInterview();

        public async Task<Interview> getInterviewById(int Interview_id) => await _interviewRepository.getInterviewById(Interview_id);

        public async Task<Interview> saveInterview(InterviewDTO interviewDTO) {
            var newCandidate_Application_Status = await _candidate_Application_StatusRepository.getCandidate_Application_StatusById(interviewDTO.Application_id);
            if(newCandidate_Application_Status == null)
            throw new Exception("Application of the Candidate Not Found");

            var newInterview_Type = await _interview_TypeRepository.getInterview_TypeById(interviewDTO.Interview_Type_id);
            if(newInterview_Type == null)
            throw new Exception("Interview Type Not Found");

            var newInterview_Status = await _interview_StatusRepository.getInterview_StatusById(interviewDTO.Interview_Status_id);
            if(newInterview_Status == null)
            throw new Exception("Interview Status Not Found");
            Console.WriteLine(interviewDTO.Application_id);

            Interview interview = new Interview{
                Application_id = interviewDTO.Application_id,
                candidate_Application_Status = newCandidate_Application_Status,
                Number_of_round = interviewDTO.Number_of_round,
                Interview_Type_id = interviewDTO.Interview_Type_id,
                interview_Type = newInterview_Type,
                Scheduled_at = interviewDTO.Scheduled_at,
                Interview_Status_id = interviewDTO.Interview_Status_id,
                interview_Status = newInterview_Status,
                Special_Notes = interviewDTO.Special_Notes,
                Interview_Meeting_Link = interviewDTO.Interview_Meeting_Link,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _interviewRepository.saveInterview(interview);
        }
        public async Task<Interview> updateInterview(int Interview_id, InterviewDTO interviewDTO) {
            var existingInterview = await _interviewRepository.getInterviewById(Interview_id);
            if(existingInterview == null)
            throw new Exception("Interview Not Found");

            var newCandidate_Application_Status = await _candidate_Application_StatusRepository.getCandidate_Application_StatusById(interviewDTO.Application_id);
            if(newCandidate_Application_Status == null)
            throw new Exception("Application of the Candidate Not Found");

            var newInterview_Type = await _interview_TypeRepository.getInterview_TypeById(interviewDTO.Interview_Type_id);
            if(newInterview_Type == null)
            throw new Exception("Interview Type Not Found");

            var newInterview_Status = await _interview_StatusRepository.getInterview_StatusById(interviewDTO.Interview_Status_id);
            if(newInterview_Status == null)
            throw new Exception("Interview Status Not Found");

            existingInterview.Application_id = interviewDTO.Application_id;
            existingInterview.candidate_Application_Status = newCandidate_Application_Status;
            existingInterview.Number_of_round = interviewDTO.Number_of_round;
            existingInterview.Interview_Type_id = interviewDTO.Interview_Type_id;
            existingInterview.interview_Type = newInterview_Type;
            existingInterview.Scheduled_at = interviewDTO.Scheduled_at;
            existingInterview.Interview_Status_id = interviewDTO.Interview_Status_id;
            existingInterview.interview_Status = newInterview_Status;
            existingInterview.Special_Notes = interviewDTO.Special_Notes;
            existingInterview.Interview_Meeting_Link = interviewDTO.Interview_Meeting_Link;
            existingInterview.Updated_at = DateTime.Now;

            return await _interviewRepository.updateInterview(existingInterview);
        }
    }
}