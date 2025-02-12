using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IInterviewService
    {
        Task<IEnumerable<Interview>> getAllInterview();

        Task<Interview> getInterviewById(int Interview_id);

        Task<Interview> saveInterview(InterviewDTO interviewDTO);

        Task<Interview> updateInterview(int Interview_id,InterviewDTO interviewDTO);

        Task<bool> deleteInterview(int Interview_id);

        Task sendNotification(Interview interview, string Action);

        Task<IEnumerable<Interview_Status>> getAllInterview_Status();

        Task<Interview_Status> getInterview_StatusById(int Interview_Status_id);

        Task<Interview_Status> saveInterview_Status(Interview_Status interview_Status);

        Task<IEnumerable<Interview_Type>> getAllInterview_Type();

        Task<Interview_Type> getInterview_TypeById(int Interview_Type_id);

        Task<Interview_Type> saveInterview_Type(Interview_Type interview_Type);

        Task<IEnumerable<Interviewer>> getAllInterviewer();

        Task<Interviewer> getInterviewerById(int Interviewer_id);

        Task<Interviewer> saveInterviewer(InterviewerDTO interviewerDTO);

        Task<Interviewer> updateInterviewer(int Interviewer_id,InterviewerDTO interviewerDTO);

        Task<bool> deleteInterviewer(int Interviewer_id);

        Task sendNotification(Interviewer interviewer,string Action);

        Task interviewUpdated(Interview interview,string Action);

        Task<IEnumerable<Feedback>> getAllFeedback();

        Task<Feedback> getFeedbackById(int Feedback_id);

        Task<Feedback> saveFeedback(FeedbackDTO feedbackDTO);

        Task<Feedback> updateFeedback(int Feedback_id,FeedbackDTO feedbackDTO);

        Task<bool> deleteFeedback(int Feedback_id);
    }
}