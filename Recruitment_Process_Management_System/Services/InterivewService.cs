using System.Threading.Tasks;
using Recruitment_Process_Management_System.Controllers;
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
        private readonly INotification_CandidateService _notification_CandidateService;
        private readonly IInterviewerRepository _interviewerRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotifications_UsersService _notifications_UsersService;
                private readonly IFeedbackRepository _feedbackRepository;


        public InterviewService(IInterviewRepository interviewRepository,
        ICandidate_Application_StatusRepository candidate_Application_StatusRepository,
        IInterview_StatusRepository interview_StatusRepository,
        IInterview_TypeRepository interview_TypeRepository,
        INotification_CandidateService notification_CandidateService,
        IInterviewerRepository interviewerRepository,IFeedbackRepository feedbackRepository,
        IUserRepository userRepository,INotifications_UsersService notifications_UsersService)
        {
            _interviewRepository = interviewRepository;
            _candidate_Application_StatusRepository = candidate_Application_StatusRepository;
            _interview_StatusRepository = interview_StatusRepository;
            _interview_TypeRepository = interview_TypeRepository;
            _notification_CandidateService = notification_CandidateService;
            _interviewerRepository = interviewerRepository;
            _userRepository = userRepository;
            _notifications_UsersService = notifications_UsersService;
            _feedbackRepository = feedbackRepository;
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
            await sendNotification(interview,"Scheduled");
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

            if(!(existingInterview.Scheduled_at != interviewDTO.Scheduled_at))
            {
                await sendNotification(existingInterview,"Rescheduled");
                await interviewUpdated(existingInterview,"Rescheduled");
            }
            return await _interviewRepository.updateInterview(existingInterview);
        }

        public async Task sendNotification(Interview interview,string Action)
        {
            string message = "Your Interview has been " + Action + " on " + interview.Scheduled_at.ToString() + " For Job Title " + interview.candidate_Application_Status.job.Job_title + ".\n This is " + interview.interview_Type.Interview_Type_name + " and This is Round Number " + interview.Number_of_round.ToString();
            Notifications_CandidateDTO notifications_CandidateDTO = new Notifications_CandidateDTO
            {
                Candidate_id = interview.candidate_Application_Status.Candidate_id,
                Message = message,
                Status = false
            };

            await _notification_CandidateService.saveCandidateNotification(notifications_CandidateDTO);
        }

        public async Task<IEnumerable<Interview_Status>> getAllInterview_Status() => await _interview_StatusRepository.getAllInterview_Status();

        public async Task<Interview_Status> getInterview_StatusById(int Interview_Status_id) => await _interview_StatusRepository.getInterview_StatusById(Interview_Status_id);

        public async Task<Interview_Status> saveInterview_Status(Interview_Status interview_Status) {

            return await _interview_StatusRepository.saveInterview_Status(interview_Status);
        }

        public async Task<IEnumerable<Interview_Type>> getAllInterview_Type() => await _interview_TypeRepository.getAllInterview_Type();

        public async Task<Interview_Type> getInterview_TypeById(int Interview_Type_id) => await _interview_TypeRepository.getInterview_TypeById(Interview_Type_id);

        public async Task<Interview_Type> saveInterview_Type(Interview_Type interview_Type) {
           return await _interview_TypeRepository.saveInterview_Type(interview_Type);
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

            await sendNotification(interviewer,"Scheduled");
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

        public async Task interviewUpdated(Interview interview,string Action)
        {
            var responses = await _interviewerRepository.getAllInterviewer();
            var existingInterview = responses.FirstOrDefault(r => r.Interview_id == interview.Interview_id);
            existingInterview.interview = interview;
            await sendNotification(existingInterview,Action);
        }

        public async Task sendNotification(Interviewer interviewer,string Action)
        {
            string message = "You Need to take the interview for Job Title " + interviewer.interview.candidate_Application_Status.job.Job_title + " which is " + Action + " on " + interviewer.interview.Scheduled_at.ToString() + " \n The Interview Type is " + interviewer.interview.interview_Type.Interview_Type_name + " and The Round Number is " + interviewer.interview.Number_of_round.ToString();
            Notifications_UsersDTO notifications_UsersDTO = new Notifications_UsersDTO{
                User_id = interviewer.User_id,
                Message = message,
                Status = false
            };

            await _notifications_UsersService.saveUserNotification(notifications_UsersDTO);
        }

        public async Task<bool> deleteFeedback(int Feedback_id)
        {
            var feedback = await _feedbackRepository.getFeedbackById(Feedback_id);
            if(feedback == null)
            return false;

            return await _feedbackRepository.deleteFeedback(feedback);
        }

        public async Task<IEnumerable<Feedback>> getAllFeedback() => await _feedbackRepository.getAllFeedback();

        public async Task<Feedback> getFeedbackById(int Feedback_id) => await _feedbackRepository.getFeedbackById(Feedback_id);

        public async Task<Feedback> saveFeedback(FeedbackDTO feedbackDTO)
        {
            var newInterview = await _interviewRepository.getInterviewById(feedbackDTO.Interview_id);
            if(newInterview == null)
            throw new Exception("Interview Not Found");

            var newUser = await _userRepository.getUserById(feedbackDTO.User_id);
            if(newUser == null)
            throw new Exception("User Not Found");

            Feedback feedback = new Feedback{
                Interview_id = feedbackDTO.Interview_id,
                interview = newInterview,
                User_id = feedbackDTO.User_id,
                user = newUser,
                Technology = feedbackDTO.Technology,
                rating = feedbackDTO.rating,
                comments = feedbackDTO.comments,
                submitted_date = feedbackDTO.submitted_date,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            return await _feedbackRepository.saveFeedback(feedback);
        }

        public async Task<Feedback> updateFeedback(int Feedback_id, FeedbackDTO feedbackDTO)
        {
            var newInterview = await _interviewRepository.getInterviewById(feedbackDTO.Interview_id);
            if(newInterview == null)
            throw new Exception("Interview Not Found");

            var newUser = await _userRepository.getUserById(feedbackDTO.User_id);
            if(newUser == null)
            throw new Exception("User Not Found");

            var existingFeedback = await _feedbackRepository.getFeedbackById(Feedback_id);
            if(existingFeedback == null)
            throw new Exception("Feedback Not Found");

            existingFeedback.Interview_id = feedbackDTO.Interview_id;
            existingFeedback.interview = newInterview;
            existingFeedback.User_id = feedbackDTO.User_id;
            existingFeedback.user = newUser;
            existingFeedback.Technology = feedbackDTO.Technology;
            existingFeedback.rating = feedbackDTO.rating;
            existingFeedback.comments = feedbackDTO.comments;
            existingFeedback.submitted_date = feedbackDTO.submitted_date;
            existingFeedback.Updated_at = DateTime.Now;

            return await _feedbackRepository.updateFeedback(existingFeedback);
        }
    }
}