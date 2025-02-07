using System.Reflection.Metadata;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IDocument_SubmittedService
    {
        IEnumerable<Document_Submitted> getAllDocument_Submitted();

        Document_Submitted getDocument_SubmittedById(int Document_Submitted_id);

        Document_Submitted saveDocument_Submitted(Document_SubmittedDTO document_SubmittedDTO);

        Document_Submitted updateDocument_Submitted(int Document_Submitted_id,Document_SubmittedDTO document_SubmittedDTO);

        bool deleteDocument_Submitted(int Document_Submitted_id);
    }
}