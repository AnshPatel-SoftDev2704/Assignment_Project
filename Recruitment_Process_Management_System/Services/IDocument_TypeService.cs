using System.Reflection.Metadata;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IDocument_TypeService
    {
        Task<IEnumerable<Document_Type>> getAllDocument_Type();

        Task<Document_Type> getDocument_TypeById(int Document_Type_id);

        Task<Document_Type> saveDocumenty_Type(Document_Type document_Type);

        Task<Document_Type> updateDocument_Type(int Document_Type_id,Document_Type document_Type);

        Task<bool> deleteDocument_Type(int Document_Type_id);
    }
}