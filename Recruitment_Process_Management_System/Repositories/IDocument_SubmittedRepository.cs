using System.Reflection.Metadata;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IDocument_SubmittedRepository
    {
        Task<IEnumerable<Document_Submitted>> getAllDocument_Submitted();

        Task<Document_Submitted> getDocument_SubmittedById(int Document_Submitted_id);

        Task<Document_Submitted> saveDocument_Submitted(Document_Submitted document_Submitted);

        Task<Document_Submitted> updateDocument_Submitted(Document_Submitted document_Submitted);

        Task deleteDocument_Submitted(Document_Submitted document_Submitted);
    }
}