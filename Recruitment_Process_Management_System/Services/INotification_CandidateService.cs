using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface INotification_CandidateService
    {
        Task<IEnumerable<Notifications_Candidate>> getAllCandidateNotifications();

        Task<Notifications_Candidate> getCandidateNotificationById(int Notifications_Candidate_id);

        Task<Notifications_Candidate> saveCandidateNotification(Notifications_CandidateDTO notifications_CandidateDTO);

        Task<Notifications_Candidate> updateCandidateNotification(int Notifications_Candidate_id,Notifications_CandidateDTO notifications_CandidateDTO);

        Task<bool> deleteCandidateNotification(int Notifications_Candidate_id);
    }
}