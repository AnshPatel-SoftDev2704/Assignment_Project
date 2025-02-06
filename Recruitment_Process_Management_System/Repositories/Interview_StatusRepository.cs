using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Interview_StatusRepository : IInterview_StatusRepository
    {
        private readonly ApplicationDbContext _context ;

        public Interview_StatusRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public bool deleteInterview_Status(int Interview_Status_id)
        {
            var response = _context.Interview_Status.FirstOrDefault(i => i.Interview_Status_id == Interview_Status_id);
            if(response == null)
            return false;

            _context.Interview_Status.Remove(response);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Interview_Status> getAllInterview_Status()
        {
            var response = _context.Interview_Status.ToList();
            return response;
        }

        public Interview_Status getInterview_StatusById(int Interview_Status_id)
        {
            var response = _context.Interview_Status.FirstOrDefault(i => i.Interview_Status_id == Interview_Status_id);
            return response;
        }

        public Interview_Status saveInterview_Status(Interview_Status interview_Status)
        {
            var response = _context.Interview_Status.FirstOrDefault(i => i.Interview_Status_name == interview_Status.Interview_Status_name);
            if(response != null)
            return null;

            interview_Status.Created_at = DateTime.Now;
            interview_Status.Updated_at = DateTime.Now;
            _context.Interview_Status.Add(interview_Status);
            _context.SaveChanges();
            return interview_Status;
        }

        public Interview_Status updateInterview_Status(int Interview_Status_id, Interview_Status interview_Status)
        {
            var response = _context.Interview_Status.FirstOrDefault(i => i.Interview_Status_id == Interview_Status_id);
            if(response == null)
            return null;

            response.Interview_Status_name = interview_Status.Interview_Status_name;
            response.Interview_Status_description = interview_Status.Interview_Status_description;
            response.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return response;
        }
    }
}