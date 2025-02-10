using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IInterviewService
    {
        Task<IEnumerable<Interview>> getAllInterview();

        Task<Interview> getInterviewById(int Interview_id);

        Task<Interview> saveInterview(InterviewDTO interviewDTO);

        Task<Interview> updateInterview(int Interview_id,InterviewDTO interviewDTO);

        Task<bool> deleteInterview(int Interview_id);

        Task sendNotification(Interview interview, string Action);
    }
}