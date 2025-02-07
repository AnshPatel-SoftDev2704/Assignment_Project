using Microsoft.VisualBasic;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Document_SubmittedService : IDocument_SubmittedService
    {
        private readonly IDocument_SubmittedRepository _document_SubmittedRepository;
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;
        private readonly IDocument_TypeRepository _document_TypeRepository;
        private readonly IJobsRepository _jobsRepository;
        private readonly IUserRepository _userRepository;

        public Document_SubmittedService(IDocument_SubmittedRepository document_SubmittedRepository,ICandidate_DetailsRepository candidate_DetailsRepository,IDocument_TypeRepository document_TypeRepository,IJobsRepository jobsRepository,IUserRepository userRepository)
        {
            _document_SubmittedRepository = document_SubmittedRepository;
            _candidate_DetailsRepository = candidate_DetailsRepository;
            _document_TypeRepository = document_TypeRepository;
            _jobsRepository = jobsRepository;
            _userRepository = userRepository;
        }

        public bool deleteDocument_Submitted(int Document_Submitted_id)
        {
            var response = _document_SubmittedRepository.getDocument_SubmittedById(Document_Submitted_id);
            if(response == null)
            return false;

            _document_SubmittedRepository.deleteDocument_Submitted(response);
            return true;
        }

        public IEnumerable<Document_Submitted> getAllDocument_Submitted()
        {
            var responses = _document_SubmittedRepository.getAllDocument_Submitted();
            return responses;
        }

        public Document_Submitted getDocument_SubmittedById(int Document_Submitted_id)
        {
            var response = _document_SubmittedRepository.getDocument_SubmittedById(Document_Submitted_id);
            return response;
        }

        public Document_Submitted saveDocument_Submitted(Document_SubmittedDTO document_SubmittedDTO)
        {
            var newCandidate = _candidate_DetailsRepository.getCandidate_DetailsById(document_SubmittedDTO.Candidate_id);
            var newDocument_type = _document_TypeRepository.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
            var newJob = _jobsRepository.getJobById(document_SubmittedDTO.Job_id);
            var newUser = _userRepository.getUserById(document_SubmittedDTO.Approved_by);

            Document_Submitted document_Submitted = new Document_Submitted{
                Candidate_id = document_SubmittedDTO.Candidate_id,
                candidate = newCandidate,
                Job_id = document_SubmittedDTO.Job_id,
                job = newJob,
                Document_Type_id = document_SubmittedDTO.Document_Type_id,
                document_Type = newDocument_type,
                Status = document_SubmittedDTO.Status,
                Approved_by = document_SubmittedDTO.Approved_by,
                user = newUser,
                Document_path = document_SubmittedDTO.Document_path,
                Submitted_date = document_SubmittedDTO.Submitted_date,
                Approved_date = document_SubmittedDTO.Approved_date,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            _document_SubmittedRepository.saveDocument_Submitted(document_Submitted);
            return document_Submitted;
        }

        public Document_Submitted updateDocument_Submitted(int Document_Submitted_id, Document_SubmittedDTO document_SubmittedDTO)
        {
            var newCandidate = _candidate_DetailsRepository.getCandidate_DetailsById(document_SubmittedDTO.Candidate_id);
            var newDocument_type = _document_TypeRepository.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
            var newJob = _jobsRepository.getJobById(document_SubmittedDTO.Job_id);
            var newUser = _userRepository.getUserById(document_SubmittedDTO.Approved_by);
            var existingDocument_Submitted = _document_SubmittedRepository.getDocument_SubmittedById(Document_Submitted_id);
            if(existingDocument_Submitted == null)
            return null;

            existingDocument_Submitted.Candidate_id = document_SubmittedDTO.Candidate_id;
            existingDocument_Submitted.candidate = newCandidate;
            existingDocument_Submitted.Job_id = document_SubmittedDTO.Job_id;
            existingDocument_Submitted.job = newJob;
            existingDocument_Submitted.Document_Type_id = document_SubmittedDTO.Document_Type_id;
            existingDocument_Submitted.document_Type = newDocument_type;
            existingDocument_Submitted.Status = document_SubmittedDTO.Status;
            existingDocument_Submitted.Approved_by = document_SubmittedDTO.Approved_by;
            existingDocument_Submitted.user = newUser;
            existingDocument_Submitted.Document_path = document_SubmittedDTO.Document_path;
            existingDocument_Submitted.Submitted_date = document_SubmittedDTO.Submitted_date;
            existingDocument_Submitted.Approved_date = document_SubmittedDTO.Approved_date;
            existingDocument_Submitted.Updated_at = DateTime.Now;

            _document_SubmittedRepository.updateDocument_Submitted(existingDocument_Submitted);
            return existingDocument_Submitted;
        }
    }
}