using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Candidate_Application_StatusService : ICandidate_Application_StatusService
    {
        private readonly ICandidate_Application_StatusRepository _candidate_Application_StatusRepository;

        public Candidate_Application_StatusService(ICandidate_Application_StatusRepository candidate_Application_StatusRepository)
        {
            _candidate_Application_StatusRepository = candidate_Application_StatusRepository;
        }

        public bool deleteCandidate_Application_Status(int Candidate_Application_Status_id) => _candidate_Application_StatusRepository.deleteCandidate_Application_Status(Candidate_Application_Status_id);

        public IEnumerable<Candidate_Application_Status> getAllCandidate_Application_Status() => _candidate_Application_StatusRepository.getAllCandidate_Application_Status();

        public Candidate_Application_Status getCandidate_Application_StatusById(int Candidate_Application_Status_id) => _candidate_Application_StatusRepository.getCandidate_Application_StatusById(Candidate_Application_Status_id);

        public Candidate_Application_Status saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO) => _candidate_Application_StatusRepository.saveCandidate_Application_Status(candidate_Application_StatusDTO);

        public Candidate_Application_Status updateCandidate_Application_Status(int Candidate_Application_Status_id, Candidate_Application_StatusDTO candidate_Application_StatusDTO) => _candidate_Application_StatusRepository.updateCandidate_Application_Status(Candidate_Application_Status_id,candidate_Application_StatusDTO);
    }
}