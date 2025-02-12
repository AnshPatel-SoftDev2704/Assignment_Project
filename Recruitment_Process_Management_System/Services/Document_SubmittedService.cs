using System.Threading.Tasks;
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

        public async Task<bool> deleteDocument_Submitted(int Document_Submitted_id)
        {
            var response = await _document_SubmittedRepository.getDocument_SubmittedById(Document_Submitted_id);
            if(response == null)
            return false;

            await _document_SubmittedRepository.deleteDocument_Submitted(response);
            return true;
        }

        public async Task<IEnumerable<Document_Submitted>> getAllDocument_Submitted()
        {
            var responses = await _document_SubmittedRepository.getAllDocument_Submitted();
            return responses;
        }

        public async Task<Document_Submitted> getDocument_SubmittedById(int Document_Submitted_id)
        {
            var response = await _document_SubmittedRepository.getDocument_SubmittedById(Document_Submitted_id);
            return response;
        }

        public async Task<Document_Submitted> saveDocument_Submitted(Document_SubmittedDTO document_SubmittedDTO)
        {
            var newCandidate = await _candidate_DetailsRepository.getCandidate_DetailsById(document_SubmittedDTO.Candidate_id);
            var newDocument_type = await _document_TypeRepository.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
            var newJob = await _jobsRepository.getJobById(document_SubmittedDTO.Job_id);
            var newUser = await _userRepository.getUserById(document_SubmittedDTO.Approved_by);

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

        public async Task<Document_Submitted> updateDocument_Submitted(int Document_Submitted_id, Document_SubmittedDTO document_SubmittedDTO)
        {
            var newCandidate = await _candidate_DetailsRepository.getCandidate_DetailsById(document_SubmittedDTO.Candidate_id);
            var newDocument_type = await _document_TypeRepository.getDocument_TypeById(document_SubmittedDTO.Document_Type_id);
            var newJob = await _jobsRepository.getJobById(document_SubmittedDTO.Job_id);
            var newUser = await _userRepository.getUserById(document_SubmittedDTO.Approved_by);
            var existingDocument_Submitted = await _document_SubmittedRepository.getDocument_SubmittedById(Document_Submitted_id);
            if(existingDocument_Submitted == null)
            throw new Exception("This document is already submitted by the Candidate");

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

            return await _document_SubmittedRepository.updateDocument_Submitted(existingDocument_Submitted);
        }

        public async Task<bool> deleteDocument_Type(int Document_Type_id)
        {
            var response = await _document_TypeRepository.getDocument_TypeById(Document_Type_id);
            if(response == null)
            return false;

            _document_TypeRepository.deleteDocument_Type(response);
            return true;
        }

        public async Task<IEnumerable<Document_Type>> getAllDocument_Type()
        {
            var responses = await _document_TypeRepository.getAllDocument_Type();
            return responses;
        }

        public async Task<Document_Type> getDocument_TypeById(int Document_Type_id)
        {
            var response = await _document_TypeRepository.getDocument_TypeById(Document_Type_id);
            return response;
        }

        public async Task<Document_Type> saveDocumenty_Type(Document_Type document_Type)
        {
            var responses = await _document_TypeRepository.getAllDocument_Type();
            var result = responses.FirstOrDefault(r => r.Document_name == document_Type.Document_name);
            if(result != null)
            return null;

            document_Type.Created_at = DateTime.Now;
            document_Type.Updated_at = DateTime.Now;
            var response = await _document_TypeRepository.saveDocument_Type(document_Type);
            return response;
        }

        public async Task<Document_Type> updateDocument_Type(int Document_Type_id,Document_Type document_Type)
        {
            var responses = await  _document_TypeRepository.getAllDocument_Type();
            var existingDocument_Type = responses.FirstOrDefault(r => r.Document_Type_id == Document_Type_id);
            if(existingDocument_Type == null)
            return null;

            existingDocument_Type.Document_name = document_Type.Document_name;
            existingDocument_Type.Document_type = document_Type.Document_type;
            existingDocument_Type.Updated_at = DateTime.Now;

            var response =await  _document_TypeRepository.updateDocument_Type(existingDocument_Type);
            return response;
        }
    }
}