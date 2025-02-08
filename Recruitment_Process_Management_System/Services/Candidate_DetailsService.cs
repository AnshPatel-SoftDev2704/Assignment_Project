using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Candidate_DetailsService : ICandidate_DetailsService
    {
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;
        private readonly IRoleRepository _roleRepository;
        public Candidate_DetailsService(ICandidate_DetailsRepository candidate_DetailsRepository,IRoleRepository roleRepository)
        {
            _candidate_DetailsRepository = candidate_DetailsRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> deleteCandidate_Details(int Candidate_id) {
            var candidate = await _candidate_DetailsRepository.getCandidate_DetailsById(Candidate_id);
            if(candidate == null)
            return false;
            return await _candidate_DetailsRepository.deleteCandidate_Details(candidate);
        }

        public async Task<IEnumerable<Candidate_Details>> getAllCandidate_Details() => await _candidate_DetailsRepository.getAllCandidate_Details();

        public async Task<Candidate_Details> getCandidate_DetailsById(int Candidate_id) => await _candidate_DetailsRepository.getCandidate_DetailsById(Candidate_id);

        public async Task<Candidate_Details> saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO) {
            var responseRole = await _roleRepository.getRoleById(candidate_DetailsDTO.Role_id);
            if(responseRole == null)
            throw new Exception("Role Not Found");

            Candidate_Details newCandidate = new Candidate_Details{
                Candidate_name = candidate_DetailsDTO.Candidate_name,
                Candidate_DOB = candidate_DetailsDTO.Candidate_DOB,
                Candidate_email = candidate_DetailsDTO.Candidate_email,
                Candidate_password = candidate_DetailsDTO.Candidate_password,
                Candidate_Total_Work_Experience = candidate_DetailsDTO.Candidate_Total_Work_Experience,
                PhoneNo = candidate_DetailsDTO.PhoneNo,
                CV_Path = candidate_DetailsDTO.CV_Path,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Role_id = candidate_DetailsDTO.Role_id,
                role = responseRole
            };
            return await _candidate_DetailsRepository.saveCandidate_Details(newCandidate);
        }

        public async Task<Candidate_Details> updateCandidate_Details(int Candidate_id, Candidate_DetailsDTO candidate_DetailsDTO) {
            var existingCandidate = await _candidate_DetailsRepository.getCandidate_DetailsById(Candidate_id);
            if(existingCandidate == null)
            throw new Exception("Candidate Not Found");

            var responseRole = await _roleRepository.getRoleById(candidate_DetailsDTO.Role_id);
            if(responseRole == null)
            throw new Exception("Role Not Found");

            existingCandidate.Candidate_name = candidate_DetailsDTO.Candidate_name;
            existingCandidate.Candidate_DOB = candidate_DetailsDTO.Candidate_DOB;
            existingCandidate.Candidate_email = candidate_DetailsDTO.Candidate_email;
            existingCandidate.Candidate_password = candidate_DetailsDTO.Candidate_password;
            existingCandidate.Candidate_Total_Work_Experience = candidate_DetailsDTO.Candidate_Total_Work_Experience;
            existingCandidate.PhoneNo = candidate_DetailsDTO.PhoneNo;
            existingCandidate.CV_Path = candidate_DetailsDTO.CV_Path;
            existingCandidate.Updated_at = DateTime.Now;
            existingCandidate.Role_id = candidate_DetailsDTO.Role_id;
            existingCandidate.role = responseRole;
            return await _candidate_DetailsRepository.updateCandidate_Details(existingCandidate);
        }
    }
}