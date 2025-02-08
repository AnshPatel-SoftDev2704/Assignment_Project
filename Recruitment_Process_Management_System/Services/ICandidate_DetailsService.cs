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
    }
}