using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface ICandidate_DetailsRepository
    {
        IEnumerable<Candidate_Details> getAllCandidate_Details();
        Candidate_Details getCandidate_DetailsById(int Candidate_id);
        Candidate_Details saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO);
        Candidate_Details updateCandidate_Details(int Candidate_id,Candidate_DetailsDTO candidate_DetailsDTO);
        bool deleteCandidate_Details(int Candidate_id);
    }
}