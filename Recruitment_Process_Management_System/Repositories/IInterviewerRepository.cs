using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IInterviewerRepository
    {
        Task<IEnumerable<Interviewer>> getAllInterviewer();

        Task<Interviewer> getInterviewerById(int Interviewer_id);

        Task<Interviewer> saveInterviewer(Interviewer interviewer);

        Task<Interviewer> updateInterviewer(Interviewer interviewer);

        Task<bool> deleteInterviewer(Interviewer interviewer);
    }
}