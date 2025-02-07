using System.Reflection.Metadata;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IDocument_SubmittedRepository
    {
        IEnumerable<Document_Submitted> getAllDocument_Submitted();

        Document_Submitted getDocument_SubmittedById(int Document_Submitted_id);

        Document_Submitted saveDocument_Submitted(Document_Submitted document_Submitted);

        Document_Submitted updateDocument_Submitted(Document_Submitted document_Submitted);

        void deleteDocument_Submitted(Document_Submitted document_Submitted);
    }
}