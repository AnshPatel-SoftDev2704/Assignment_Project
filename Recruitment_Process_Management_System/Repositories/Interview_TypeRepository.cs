using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.VisualBasic;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Interview_TyperRepository : IInterview_TyperRepository
    {
        private readonly ApplicationDbContext _context;

        public Interview_TyperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteInterview_Type(int Interview_Type_id)
        {
            var response = _context.Interview_Type.FirstOrDefault(it => it.Interview_Type_id == Interview_Type_id);
            if(response == null)
            return false;

            _context.Interview_Type.Remove(response);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Interview_Type> getAllInterview_Type()
        {
            var response = _context.Interview_Type.ToList();
            return response;
        }

        public Interview_Type getInterview_TypeById(int Interview_Type_id)
        {
            var response = _context.Interview_Type.FirstOrDefault(it => it.Interview_Type_id == Interview_Type_id);
            return response;
        }

        public Interview_Type saveInterview_Type(Interview_Type interview_Type)
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

        public Interview_Type updateInterview_Type(int Interview_Type_id, Interview_Type interview_Type)
        {
            var response = _context.Interview_Type.FirstOrDefault(it => it.Interview_Type_id == Interview_Type_id);
            if(response == null)
            return null;

            response.Interview_Type_name = interview_Type.Interview_Type_name;
            response.Interview_Type_description = interview_Type.Interview_Type_description;
            response.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return response;
        }
    }
}