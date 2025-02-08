using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Services;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IInterviewRepository
    {
        Task<IEnumerable<Interview>> getAllInterview();

        Task<Interview> getInterviewById(int Interview_id);

        Task<Interview> saveInterview(Interview interview);

        Task<Interview> updateInterview(Interview interview);

        Task<bool> deleteInterview(Interview interview);
    }
}