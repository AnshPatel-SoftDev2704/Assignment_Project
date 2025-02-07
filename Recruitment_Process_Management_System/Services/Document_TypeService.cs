using Microsoft.VisualBasic;
using Recruitment_Process_Management_System.mdoels;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Document_TypeService : IDocument_TypeService
    {
        private readonly IDocument_TypeRepository _document_TypeRepository;

        public Document_TypeService(IDocument_TypeRepository document_TypeRepository)
        {
            _document_TypeRepository = document_TypeRepository;
        }

        public bool deleteDocument_Type(int Document_Type_id)
        {
            var response = _document_TypeRepository.getDocument_TypeById(Document_Type_id);
            if(response == null)
            return false;

            _document_TypeRepository.deleteDocument_Type(response);
            return true;
        }

        public IEnumerable<Document_Type> getAllDocument_Type()
        {
            var responses = _document_TypeRepository.getAllDocument_Type();
            return responses;
        }

        public Document_Type getDocument_TypeById(int Document_Type_id)
        {
            var response = _document_TypeRepository.getDocument_TypeById(Document_Type_id);
            return response;
        }

        public Document_Type saveDocumenty_Type(Document_Type document_Type)
        {
            var responses = _document_TypeRepository.getAllDocument_Type();
            var result = responses.FirstOrDefault(r => r.Document_name == document_Type.Document_name);
            if(result != null)
            return null;

            document_Type.Created_at = DateTime.Now;
            document_Type.Updated_at = DateTime.Now;
            var response = _document_TypeRepository.saveDocument_Type(document_Type);
            return response;
        }

        public Document_Type updateDocument_Type(int Document_Type_id,Document_Type document_Type)
        {
            var responses = _document_TypeRepository.getAllDocument_Type();
            var existingDocument_Type = responses.FirstOrDefault(r => r.Document_Type_id == Document_Type_id);
            if(existingDocument_Type == null)
            return null;

            existingDocument_Type.Document_name = document_Type.Document_name;
            existingDocument_Type.Document_type = document_Type.Document_type;
            existingDocument_Type.Updated_at = DateTime.Now;

            var response = _document_TypeRepository.updateDocument_Type(existingDocument_Type);
            return response;
        }
    }
}