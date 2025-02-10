using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface ICandidate_Application_StatusService
    {
        Task<IEnumerable<Candidate_Application_Status>> getAllCandidate_Application_Status();

        Task<Candidate_Application_Status> getCandidate_Application_StatusById(int Candidate_Application_Status_id);

        Task<Candidate_Application_Status> saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO);

        Task<Candidate_Application_Status> updateCandidate_Application_Status(int Candidate_Application_Status_id,Candidate_Application_StatusDTO candidate_Application_StatusDTO);

        Task<bool> deleteCandidate_Application_Status(int Candidate_Application_Status_id);

        Task sendNotification(Candidate_Application_Status candidate_Application_Status);
    }
}