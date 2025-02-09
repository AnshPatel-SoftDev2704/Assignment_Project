using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface INotification_CandidateRepository
    {
        Task<IEnumerable<Notifications_Candidate>> getAllCandidateNotifications();

        Task<Notifications_Candidate> getCandidateNotificationById(int Notifications_Candidate_id);

        Task<Notifications_Candidate> saveCandidateNotification(Notifications_Candidate notifications_Candidate);

        Task<Notifications_Candidate> updateCandidateNotification(Notifications_Candidate notifications_Candidate);

        Task<bool> deleteCandidateNotification(Notifications_Candidate notifications_Candidate);
    }
}