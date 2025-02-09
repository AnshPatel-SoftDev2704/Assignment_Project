using System.Threading.Tasks;
using Azure;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Selected_CandidateService : ISelected_CandidateService
    {
        private readonly ISelected_CandidateRepository _selected_CandidateRepository;
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;
        private readonly IJobsRepository _jobsRepository;

        public Selected_CandidateService(ISelected_CandidateRepository selected_CandidateRepository,
        ICandidate_DetailsRepository candidate_DetailsRepository,
        IJobsRepository jobsRepository)
        {
            _selected_CandidateRepository = selected_CandidateRepository;
            _candidate_DetailsRepository = candidate_DetailsRepository;
            _jobsRepository = jobsRepository;
        }

        public async Task<IEnumerable<Selected_Candidate>> getAllSelectedCandidate()
        {
            var responses = await _selected_CandidateRepository.getAllSelectedCandidate();
            return responses;
        }

        public async Task<Selected_Candidate> getSelectedCadidateById(int Selected_Candidate_id)
        {
            var response = await _selected_CandidateRepository.getSelecteCandidateById(Selected_Candidate_id);
            return response;
        }

        public async Task<Selected_Candidate> saveSelectedCandidate(Selected_CandidateDTO selected_CandidateDTO)
        {
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(selected_CandidateDTO.Candidate_id);
            if(candidate == null)
            throw new Exception("Candidate Not Found");

            var newJob = await _jobsRepository.getJobById(selected_CandidateDTO.Job_id);
            if(newJob == null)
            throw new Exception("Job Not Found");

            Selected_Candidate selected_Candidate = new Selected_Candidate{
                Candidate_id  = selected_CandidateDTO.Candidate_id,
                candidate_Details = candidate,
                Job_id = selected_CandidateDTO.Job_id,
                job = newJob,
                Joining_date = selected_CandidateDTO.Joining_date,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            return await _selected_CandidateRepository.saveSelectedCadidate(selected_Candidate);
        }

        public async Task<Selected_Candidate> updateSelectedCandidate(int Selected_Candidate_id,Selected_CandidateDTO selected_CandidateDTO)
        {
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(selected_CandidateDTO.Candidate_id);
            if(candidate == null)
            throw new Exception("Candidate Not Found");

            var newJob = await _jobsRepository.getJobById(selected_CandidateDTO.Job_id);
            if(newJob == null)
            throw new Exception("Job Not Found");

            var existingSelectedCandidate = await _selected_CandidateRepository.getSelecteCandidateById(Selected_Candidate_id);
            if(existingSelectedCandidate == null)
            throw new Exception("Selected Candidate Not Found");

            existingSelectedCandidate.Candidate_id  = selected_CandidateDTO.Candidate_id;
            existingSelectedCandidate.candidate_Details = candidate;
            existingSelectedCandidate.Job_id = selected_CandidateDTO.Job_id;
            existingSelectedCandidate.job = newJob;
            existingSelectedCandidate.Joining_date = selected_CandidateDTO.Joining_date;
            existingSelectedCandidate.Updated_at = DateTime.Now;

            return await _selected_CandidateRepository.updateSelectedCandidate(existingSelectedCandidate);
        }

        public async Task<bool> deleteSelectedCandidate(int Selected_Candidate_id)
        {
            var existingSelectedCandidate = await _selected_CandidateRepository.getSelecteCandidateById(Selected_Candidate_id);
            if(existingSelectedCandidate == null)
            return false;

            return await _selected_CandidateRepository.deleteSelectedCandidate(existingSelectedCandidate);
        }
    }
}
