using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface ICandidate_DetailsService
    {
        Task<IEnumerable<Candidate_Details>> getAllCandidate_Details();
        Task<Candidate_Details> getCandidate_DetailsById(int Candidate_id);
        Task<Candidate_Details> saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO);
        Task<Candidate_Details> updateCandidate_Details(int Candidate_id,Candidate_DetailsDTO candidate_DetailsDTO);
        Task<bool> deleteCandidate_Details(int Candidate_id);
        Task<IEnumerable<Application_Status>> getAllApplication_Status();

        Task<Application_Status> getApplication_StatusById(int Application_Status_id);

        Task<Application_Status> saveApplication_Status(Application_Status application_Status);

        Task<IEnumerable<Candidate_Skills>> getAllCandidate_Skills();

        Task<Candidate_Skills> getAllCandidate_SkillById(int Candidate_Skills_id);

        Task<Candidate_Skills> saveCandidate_Skill(Candidate_SkillsDTO candidate_SkillsDTO);

        Task<Candidate_Skills> updateCandidate_Skill(int Candidate_Skills_id,Candidate_SkillsDTO candidate_SkillsDTO);

        Task<bool> deleteCandidate_Skill(int Candidate_Skills_id);

        Task<IEnumerable<Candidate_Application_Status>> getAllCandidate_Application_Status();

        Task<Candidate_Application_Status> getCandidate_Application_StatusById(int Candidate_Application_Status_id);

        Task<Candidate_Application_Status> saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO);

        Task<Candidate_Application_Status> updateCandidate_Application_Status(int Candidate_Application_Status_id,Candidate_Application_StatusDTO candidate_Application_StatusDTO);

        Task<bool> deleteCandidate_Application_Status(int Candidate_Application_Status_id);

        Task sendNotification(Candidate_Application_Status candidate_Application_Status);
    }
}