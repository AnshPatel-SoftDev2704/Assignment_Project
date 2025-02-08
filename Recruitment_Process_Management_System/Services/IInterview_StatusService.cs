using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IInterview_StatusService
    {
        Task<IEnumerable<Interview_Status>> getAllInterview_Status();

        Task<Interview_Status> getInterview_StatusById(int Interview_Status_id);

        Task<Interview_Status> saveInterview_Status(Interview_Status interview_Status);

        Task<Interview_Status> updateInterview_Status(int Interview_Status_id,Interview_Status interview_Status);

        Task<bool> deleteInterview_Status(int Interview_Status_id);
    }
}