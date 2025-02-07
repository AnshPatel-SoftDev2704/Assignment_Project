using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Interview_StatusService : IInterview_StatusService
    {
        private readonly IInterview_StatusRepository _interview_StatusRepository;

        public Interview_StatusService(IInterview_StatusRepository interview_StatusRepository)
        {
            _interview_StatusRepository = interview_StatusRepository;
        }

        public bool deleteInterview_Status(int Interview_Status_id) => _interview_StatusRepository.deleteInterview_Status(Interview_Status_id);

        public IEnumerable<Interview_Status> getAllInterview_Status() => _interview_StatusRepository.getAllInterview_Status();

        public Interview_Status getInterview_StatusById(int Interview_Status_id) => _interview_StatusRepository.getInterview_StatusById(Interview_Status_id);

        public Interview_Status saveInterview_Status(Interview_Status interview_Status) => _interview_StatusRepository.saveInterview_Status(interview_Status);

        public Interview_Status updateInterview_Status(int Interview_Status_id, Interview_Status interview_Status) => _interview_StatusRepository.updateInterview_Status(Interview_Status_id,interview_Status);
    }
}