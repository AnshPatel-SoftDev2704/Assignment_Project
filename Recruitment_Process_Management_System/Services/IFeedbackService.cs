using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IFeedbackService 
    {
        Task<IEnumerable<Feedback>> getAllFeedback();

        Task<Feedback> getFeedbackById(int Feedback_id);

        Task<Feedback> saveFeedback(FeedbackDTO feedbackDTO);

        Task<Feedback> updateFeedback(int Feedback_id,FeedbackDTO feedbackDTO);

        Task<bool> deleteFeedback(int Feedback_id);

    }
}