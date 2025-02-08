using System.Reflection.Metadata;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IDocument_TypeRepository
    {
        Task<IEnumerable<Document_Type>> getAllDocument_Type();

        Task<Document_Type> getDocument_TypeById(int Document_Type_id);

        Task<Document_Type> saveDocument_Type(Document_Type document_Type);

        Task<Document_Type> updateDocument_Type(Document_Type document_Type);

        Task deleteDocument_Type(Document_Type document_Type);
    }
}