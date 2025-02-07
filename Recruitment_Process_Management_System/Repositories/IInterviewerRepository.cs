using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IInterviewerRepository
    {
        IEnumerable<Interviewer> getAllInterviewer();

        Interviewer getInterviewerById(int Interviewer_id);

        Interviewer saveInterviewer(InterviewerDTO interviewerDTO);

        Interviewer updateInterviewer(int Interviewer_id,InterviewerDTO interviewerDTO);

        bool deleteInterviewer(int Interviewer_id);
    }
}