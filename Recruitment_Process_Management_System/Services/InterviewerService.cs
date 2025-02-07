using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class InterviewerService : IInterviewerService
    {
        private readonly IInterviewerRepository _interviewerRepository;

        public InterviewerService(IInterviewerRepository interviewerRepository)
        {
            _interviewerRepository = interviewerRepository;
        }

        public bool deleteInterviewer(int Interviewer_id) => _interviewerRepository.deleteInterviewer(Interviewer_id);

        public IEnumerable<Interviewer> getAllInterviewer() => _interviewerRepository.getAllInterviewer();

        public Interviewer getInterviewerById(int Interviewer_id) => _interviewerRepository.getInterviewerById(Interviewer_id);

        public Interviewer saveInterviewer(InterviewerDTO interviewerDTO) => _interviewerRepository.saveInterviewer(interviewerDTO);

        public Interviewer updateInterviewer(int Interviewer_id, InterviewerDTO interviewerDTO) => _interviewerRepository.updateInterviewer(Interviewer_id,interviewerDTO);
    }
}