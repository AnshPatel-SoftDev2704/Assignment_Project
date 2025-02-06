using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IInterview_TypeRepository
    {
        IEnumerable<Interview_Type> getAllInterview_Type();

        Interview_Type getInterview_TypeById(int Interview_Type_id);

        Interview_Type saveInterview_Type(Interview_Type interview_Type);

        Interview_Type updateInterview_Type(int Interview_Type_id,Interview_Type interview_Type);

        bool deleteInterview_Type(int Interview_Type_id);
    }
}