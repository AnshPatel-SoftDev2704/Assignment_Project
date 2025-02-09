using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface ISelected_CandidateRepository
    {
        Task<IEnumerable<Selected_Candidate>> getAllSelectedCandidate();

        Task<Selected_Candidate> getSelecteCandidateById(int Selected_Candidate_id);

        Task<Selected_Candidate> saveSelectedCadidate(Selected_Candidate selected_Candidate);

        Task<Selected_Candidate> updateSelectedCandidate(Selected_Candidate selected_Candidate);

        Task<bool> deleteSelectedCandidate(Selected_Candidate selected_Candidate);
    }
}