using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface ICandidate_DetailsRepository
    {
        Task<IEnumerable<Candidate_Details>> getAllCandidate_Details();
        Task<Candidate_Details> getCandidate_DetailsById(int Candidate_id);
        Task<Candidate_Details> saveCandidate_Details(Candidate_Details candidate_Details);
        Task<Candidate_Details> updateCandidate_Details(Candidate_Details candidate_Details);
        Task<bool> deleteCandidate_Details(Candidate_Details candidate_Details);
        Task<Candidate_Details> GetCandidate_DetailsByName(string name);
    }
}