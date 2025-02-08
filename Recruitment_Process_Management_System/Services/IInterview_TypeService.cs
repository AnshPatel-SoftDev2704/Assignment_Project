using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IInterview_TypeService
    {
        Task<IEnumerable<Interview_Type>> getAllInterview_Type();

        Task<Interview_Type> getInterview_TypeById(int Interview_Type_id);

        Task<Interview_Type> saveInterview_Type(Interview_Type interview_Type);

        Task<Interview_Type> updateInterview_Type(int Interview_Type_id,Interview_Type interview_Type);

        Task<bool> deleteInterview_Type(int Interview_Type_id);
    }
}