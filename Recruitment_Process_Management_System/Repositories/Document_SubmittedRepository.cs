using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Document_SubmittedRepository : IDocument_SubmittedRepository
    {
        private readonly ApplicationDbContext _context;

        public Document_SubmittedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task deleteDocument_Submitted(Document_Submitted document_Submitted)
        {
            _context.Document_Submitted.Remove(document_Submitted);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Document_Submitted>> getAllDocument_Submitted()
        {
            var responses = _context.Document_Submitted.Include(ds => ds.candidate).Include(ds => ds.candidate.role).Include(ds => ds.job).Include(ds => ds.job.job_Status).Include(ds => ds.job.user).Include(ds => ds.user).ToList();
            return responses;
        }

        public async Task<Document_Submitted> getDocument_SubmittedById(int Document_Submitted_id)
        {
            var response= _context.Document_Submitted.Include(ds => ds.candidate).Include(ds => ds.candidate.role).Include(ds => ds.job).Include(ds => ds.job.job_Status).Include(ds => ds.job.user).Include(ds => ds.user).FirstOrDefault(ds => ds.Document_Submitted_id == Document_Submitted_id);
            return response;
        }

        public async Task<Document_Submitted> saveDocument_Submitted(Document_Submitted document_Submitted)
        {
            _context.Document_Submitted.Add(document_Submitted);
            _context.SaveChanges();
            return document_Submitted;
        }

        public async Task<Document_Submitted> updateDocument_Submitted(Document_Submitted document_Submitted)
        {
            _context.Document_Submitted.Update(document_Submitted);
            _context.SaveChanges();
            return document_Submitted;
        }
    }
}