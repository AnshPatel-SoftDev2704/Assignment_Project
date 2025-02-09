using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IUserRepository _userRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository,
        IInterviewRepository interviewRepository,IUserRepository userRepository)
        {
            _feedbackRepository = feedbackRepository;
            _interviewRepository = interviewRepository;
            _userRepository = userRepository;
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