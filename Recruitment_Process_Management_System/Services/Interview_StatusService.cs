using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Interview_StatusService : IInterview_StatusService
    {
        private readonly IInterview_StatusRepository _interview_StatusRepository;

        public Interview_StatusService(IInterview_StatusRepository interview_StatusRepository)
        {
            _interview_StatusRepository = interview_StatusRepository;
        }

        public async Task<bool> deleteInterview_Status(int Interview_Status_id) {
            var response = await _interview_StatusRepository.getInterview_StatusById(Interview_Status_id);
            if(response == null)
            return false;
            return await _interview_StatusRepository.deleteInterview_Status(response);
        }

        public async Task<IEnumerable<Interview_Status>> getAllInterview_Status() => await _interview_StatusRepository.getAllInterview_Status();

        public async Task<Interview_Status> getInterview_StatusById(int Interview_Status_id) => await _interview_StatusRepository.getInterview_StatusById(Interview_Status_id);

        public async Task<Interview_Status> saveInterview_Status(Interview_Status interview_Status) {

            return await _interview_StatusRepository.saveInterview_Status(interview_Status);
        }

        public async Task<Interview_Status> updateInterview_Status(int Interview_Status_id, Interview_Status interview_Status) {
            var response = await _interview_StatusRepository.getInterview_StatusById(Interview_Status_id);
            if(response == null)
            throw new Exception("InterView Status Not Found");

            response.Interview_Status_name = interview_Status.Interview_Status_name;
            response.Interview_Status_description = interview_Status.Interview_Status_description;
            response.Updated_at = DateTime.Now;
            return await _interview_StatusRepository.updateInterview_Status(response);
        }
    }
}