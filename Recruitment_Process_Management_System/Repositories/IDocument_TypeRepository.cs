using System.Reflection.Metadata;
using Recruitment_Process_Management_System.mdoels;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IDocument_TypeRepository
    {
        IEnumerable<Document_Type> getAllDocument_Type();

        Document_Type getDocument_TypeById(int Document_Type_id);

        Document_Type saveDocument_Type(Document_Type document_Type);

        Document_Type updateDocument_Type(Document_Type document_Type);

        void deleteDocument_Type(Document_Type document_Type);
    }
}