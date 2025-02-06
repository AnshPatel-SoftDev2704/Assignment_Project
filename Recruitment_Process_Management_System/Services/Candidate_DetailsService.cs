using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Candidate_DetailsService : ICandidate_DetailsService
    {
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;

        public Candidate_DetailsService(ICandidate_DetailsRepository candidate_DetailsRepository)
        {
            _candidate_DetailsRepository = candidate_DetailsRepository;
        }

        public bool deleteCandidate_Details(int Candidate_id) => _candidate_DetailsRepository.deleteCandidate_Details(Candidate_id);

        public IEnumerable<Candidate_Details> getAllCandidate_Details() => _candidate_DetailsRepository.getAllCandidate_Details();

        public Candidate_Details getCandidate_DetailsById(int Candidate_id) => _candidate_DetailsRepository.getCandidate_DetailsById(Candidate_id);

        public Candidate_Details saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO) => _candidate_DetailsRepository.saveCandidate_Details(candidate_DetailsDTO);

        public Candidate_Details updateCandidate_Details(int Candidate_id, Candidate_DetailsDTO candidate_DetailsDTO) => _candidate_DetailsRepository.updateCandidate_Details(Candidate_id,candidate_DetailsDTO);
    }
}