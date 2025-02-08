using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IInterviewerService
    {
        Task<IEnumerable<Interviewer>> getAllInterviewer();

        Task<Interviewer> getInterviewerById(int Interviewer_id);

        Task<Interviewer> saveInterviewer(InterviewerDTO interviewerDTO);

        Task<Interviewer> updateInterviewer(int Interviewer_id,InterviewerDTO interviewerDTO);

        Task<bool> deleteInterviewer(int Interviewer_id);
    }
}