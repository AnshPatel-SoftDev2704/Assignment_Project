using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IInterviewService
    {
        IEnumerable<Interview> getAllInterview();

        Interview getInterviewById(int Interview_id);

        Interview saveInterview(InterviewDTO interviewDTO);

        Interview updateInterview(int Interview_id,InterviewDTO interviewDTO);

        bool deleteInterview(int Interview_id);
    }
}