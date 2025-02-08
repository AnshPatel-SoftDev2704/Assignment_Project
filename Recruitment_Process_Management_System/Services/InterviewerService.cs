using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class InterviewerService : IInterviewerService
    {
        private readonly IInterviewerRepository _interviewerRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IUserRepository _userRepository;

        public InterviewerService(IInterviewerRepository interviewerRepository,
        IInterviewRepository interviewRepository,IUserRepository userRepository)
        {
            _interviewerRepository = interviewerRepository;
            _interviewRepository = interviewRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> deleteInterviewer(int Interviewer_id) {
            var response = await _interviewerRepository.getInterviewerById(Interviewer_id);
            if(response == null)
            return false;
            return await _interviewerRepository.deleteInterviewer(response);
        }

        public async Task<IEnumerable<Interviewer>> getAllInterviewer() => await _interviewerRepository.getAllInterviewer();

        public async Task<Interviewer> getInterviewerById(int Interviewer_id) => await _interviewerRepository.getInterviewerById(Interviewer_id);

        public async Task<Interviewer> saveInterviewer(InterviewerDTO interviewerDTO) {
            var newInterview = await _interviewRepository.getInterviewById(interviewerDTO.Interview_id);
            var newUser = await _userRepository.getUserById(interviewerDTO.User_id);

            if(newInterview == null)
            throw new Exception("Interview Not Found");

            if(newUser == null)
            throw new Exception("User Not Found");

            Interviewer interviewer = new Interviewer{
                Interview_id = interviewerDTO.Interview_id,
                interview = newInterview,
                User_id = interviewerDTO.User_id,
                user = newUser,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _interviewerRepository.saveInterviewer(interviewer);
        }

        public async Task<Interviewer> updateInterviewer(int Interviewer_id, InterviewerDTO interviewerDTO) {
            var newInterview = await _interviewRepository.getInterviewById(interviewerDTO.Interview_id);
            var newUser = await _userRepository.getUserById(interviewerDTO.User_id);
            var existingInterviewer = await _interviewerRepository.getInterviewerById(Interviewer_id);

            if(newInterview == null)
            throw new Exception("Interview Not Found");

            if(newUser == null)
            throw new Exception("User Not Found");

            if(existingInterviewer == null)
            throw new Exception("Interviewer Not Found");

            existingInterviewer.Interview_id = interviewerDTO.Interview_id;
            existingInterviewer.interview = newInterview;
            existingInterviewer.User_id = interviewerDTO.User_id;
            existingInterviewer.user = newUser;
            existingInterviewer.Updated_at = DateTime.Now;
            return await _interviewerRepository.updateInterviewer(existingInterviewer);
        }
    }
}