using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Interview_TyperRepository : IInterview_TypeRepository
    {
        private readonly ApplicationDbContext _context;

        public Interview_TyperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteInterview_Type(Interview_Type interview_Type)
        {
            _context.Interview_Type.Remove(interview_Type);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Interview_Type>> getAllInterview_Type()
        {
            var response = _context.Interview_Type.ToList();
            return response;
        }

        public async Task<Interview_Type> getInterview_TypeById(int Interview_Type_id)
        {
            var response = _context.Interview_Type.FirstOrDefault(it => it.Interview_Type_id == Interview_Type_id);
            return response;
        }

        public async Task<Interview_Type> saveInterview_Type(Interview_Type interview_Type)
        {
            var response = _context.Interview_Type.FirstOrDefault(it => it.Interview_Type_name == interview_Type.Interview_Type_name);
            if(response != null)
            return null;
            
            interview_Type.Created_at = DateTime.Now;
            interview_Type.Updated_at = DateTime.Now;
            _context.Interview_Type.Add(interview_Type);
            _context.SaveChanges();
            return interview_Type;
        }

        public async Task<Interview_Type> updateInterview_Type(Interview_Type interview_Type)
        {
            _context.Interview_Type.Update(interview_Type);
            _context.SaveChanges();
            return interview_Type;
        }
    }
}