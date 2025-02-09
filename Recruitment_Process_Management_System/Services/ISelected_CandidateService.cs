using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface ISelected_CandidateService
    {
        Task<IEnumerable<Selected_Candidate>> getAllSelectedCandidate();

        Task<Selected_Candidate> getSelectedCadidateById(int Selected_Candidate_id);

        Task<Selected_Candidate> saveSelectedCandidate(Selected_CandidateDTO selected_CandidateDTO);

        Task<Selected_Candidate> updateSelectedCandidate(int Selected_Candidate_id,Selected_CandidateDTO selected_CandidateDTO);

        Task<bool> deleteSelectedCandidate(int Selected_Candidate_id);
    }
}