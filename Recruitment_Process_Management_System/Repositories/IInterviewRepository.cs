using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IInterviewRepository
    {
        IEnumerable<Interview> getAllInterview();

        Interview getInterviewById(int Interview_id);

        Interview saveInterview(InterviewDTO interviewDTO);

        Interview updateInterview(int Interview_id,InterviewDTO interviewDTO);

        bool deleteInterview(int Interview_id);
    }
}