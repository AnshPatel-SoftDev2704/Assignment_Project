using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Document_TypeRespository : IDocument_TypeRepository
    {
        private readonly ApplicationDbContext _context;

        public Document_TypeRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void deleteDocument_Type(Document_Type document_Type)
        {
            _context.Document_Type.Remove(document_Type);
            _context.SaveChanges();
        }

        public IEnumerable<Document_Type> getAllDocument_Type()
        {
            var responses = _context.Document_Type.ToList();
            return responses;
        }

        public Document_Type getDocument_TypeById(int Document_Type_id)
        {
            var response = _context.Document_Type.FirstOrDefault(dt => dt.Document_Type_id == Document_Type_id);
            return response;
        }

        public Document_Type saveDocument_Type(Document_Type document_Type)
        {
            _context.Document_Type.Add(document_Type);
            _context.SaveChanges();
            return document_Type;
        }

        public Document_Type updateDocument_Type(Document_Type document_Type)
        {
            _context.Document_Type.Update(document_Type);
            _context.SaveChanges();
            return document_Type;
        }
    }
}