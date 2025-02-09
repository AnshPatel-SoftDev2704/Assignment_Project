using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> getAllFeedback();
        
        Task<Feedback> getFeedbackById(int Feedback_id);

        Task<Feedback> saveFeedback(Feedback feedback);

        Task<Feedback> updateFeedback(Feedback feedback);

        Task<bool> deleteFeedback(Feedback feedback);
    }
}