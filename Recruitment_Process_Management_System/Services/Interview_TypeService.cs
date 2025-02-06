using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Interview_TypeService : IInterview_TypeService
    {
        private readonly IInterview_TypeRepository _interview_TyperRepository;

        public Interview_TypeService(IInterview_TypeRepository interview_TyperRepository)
        {
            _interview_TyperRepository = interview_TyperRepository;
        }

        public bool deleteInterview_Type(int Interview_Type_id) => _interview_TyperRepository.deleteInterview_Type(Interview_Type_id);

        public IEnumerable<Interview_Type> getAllInterview_Type() => _interview_TyperRepository.getAllInterview_Type();

        public Interview_Type getInterview_TypeById(int Interview_Type_id) => _interview_TyperRepository.getInterview_TypeById(Interview_Type_id);

        public Interview_Type saveInterview_Type(Interview_Type interview_Type) => _interview_TyperRepository.saveInterview_Type(interview_Type);

        public Interview_Type updateInterview_Type(int Interview_Type_id, Interview_Type interview_Type) => _interview_TyperRepository.updateInterview_Type(Interview_Type_id,interview_Type);
    }
}