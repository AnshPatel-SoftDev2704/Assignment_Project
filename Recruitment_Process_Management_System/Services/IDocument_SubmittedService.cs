using System.Reflection.Metadata;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IDocument_SubmittedService
    {
        Task<IEnumerable<Document_Submitted>> getAllDocument_Submitted();

        Task<Document_Submitted> getDocument_SubmittedById(int Document_Submitted_id);

        Task<Document_Submitted> saveDocument_Submitted(Document_SubmittedDTO document_SubmittedDTO);

        Task<Document_Submitted> updateDocument_Submitted(int Document_Submitted_id,Document_SubmittedDTO document_SubmittedDTO);

        Task<bool> deleteDocument_Submitted(int Document_Submitted_id);

        Task<IEnumerable<Document_Type>> getAllDocument_Type();

        Task<Document_Type> getDocument_TypeById(int Document_Type_id);

        Task<Document_Type> saveDocumenty_Type(Document_Type document_Type);

        Task<Document_Type> updateDocument_Type(int Document_Type_id,Document_Type document_Type);

        Task<bool> deleteDocument_Type(int Document_Type_id);
    }
}