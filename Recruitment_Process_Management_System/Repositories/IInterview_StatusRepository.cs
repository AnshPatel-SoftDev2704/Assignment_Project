using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IInterview_StatusRepository
    {
        Task<IEnumerable<Interview_Status>> getAllInterview_Status();

        Task<Interview_Status> getInterview_StatusById(int Interview_Status_id);

        Task<Interview_Status> saveInterview_Status(Interview_Status interview_Status);

        Task<Interview_Status> updateInterview_Status(Interview_Status interview_Status);

        Task<bool> deleteInterview_Status(Interview_Status interview_Status);
    }
}