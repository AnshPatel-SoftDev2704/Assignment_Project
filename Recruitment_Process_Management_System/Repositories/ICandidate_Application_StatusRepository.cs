using Microsoft.AspNetCore.Mvc;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface ICandidate_Application_StatusRepository
    {
        IEnumerable<Candidate_Application_Status> getAllCandidate_Application_Status();

        Candidate_Application_Status getCandidate_Application_StatusById(int Candidate_Application_Status_id);

        Candidate_Application_Status saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO);

        Candidate_Application_Status updateCandidate_Application_Status(int Candidate_Application_Status_id,Candidate_Application_StatusDTO candidate_Application_StatusDTO);

        bool deleteCandidate_Application_Status(int Candidate_Application_Status_id);
    }
}