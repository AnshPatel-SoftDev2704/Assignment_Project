using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _interviewRepository;

        public InterviewService(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public bool deleteInterview(int Interview_id) => _interviewRepository.deleteInterview(Interview_id);

        public IEnumerable<Interview> getAllInterview() => _interviewRepository.getAllInterview();

        public Interview getInterviewById(int Interview_id) => _interviewRepository.getInterviewById(Interview_id);

        public Interview saveInterview(InterviewDTO interviewDTO) => _interviewRepository.saveInterview(interviewDTO);

        public Interview updateInterview(int Interview_id, InterviewDTO interviewDTO) => _interviewRepository.updateInterview(Interview_id,interviewDTO);
    }
}