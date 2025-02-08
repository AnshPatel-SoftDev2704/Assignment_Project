using System.Threading.Tasks;
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

        public async Task<bool> deleteInterview_Type(int Interview_Type_id) {
             var response = await _interview_TyperRepository.getInterview_TypeById(Interview_Type_id);
            if(response == null)
            return false;
           return await _interview_TyperRepository.deleteInterview_Type(response);
        }

        public async Task<IEnumerable<Interview_Type>> getAllInterview_Type() => await _interview_TyperRepository.getAllInterview_Type();

        public async Task<Interview_Type> getInterview_TypeById(int Interview_Type_id) => await _interview_TyperRepository.getInterview_TypeById(Interview_Type_id);

        public async Task<Interview_Type> saveInterview_Type(Interview_Type interview_Type) {
           return await _interview_TyperRepository.saveInterview_Type(interview_Type);
        }
        public async Task<Interview_Type> updateInterview_Type(int Interview_Type_id,Interview_Type interview_Type) {
            var response = await _interview_TyperRepository.getInterview_TypeById(Interview_Type_id);
            if(response == null)
            throw new Exception("Interview Type Not Found");

            response.Interview_Type_name = interview_Type.Interview_Type_name;
            response.Interview_Type_description = interview_Type.Interview_Type_description;
            response.Updated_at = DateTime.Now;
            return await _interview_TyperRepository.updateInterview_Type(response);
        }
    }
}