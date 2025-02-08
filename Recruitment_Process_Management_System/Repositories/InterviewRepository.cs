using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly ApplicationDbContext _context;

        public InterviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteInterview(Interview interview)
        {
            _context.Interview.Remove(interview);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Interview>> getAllInterview()
        {
            var responses = _context.Interview.Include(a => a.candidate_Application_Status).Include(i => i.interview_Status).Include(i => i.interview_Type).Include(cas => cas.candidate_Application_Status.candidate_Details).Include(cas => cas.candidate_Application_Status.job).Include(cas => cas.candidate_Application_Status.application_Status).Include(cas => cas.candidate_Application_Status.candidate_Details.role).Include(cas => cas.candidate_Application_Status.job.job_Status).Include(cas => cas.candidate_Application_Status.job.user).ToList();
            return responses;
        }

        public async Task<Interview> getInterviewById(int Interview_id)
        {
            var responses = _context.Interview.Include(a => a.candidate_Application_Status).Include(i => i.interview_Status).Include(i => i.interview_Type).Include(cas => cas.candidate_Application_Status.candidate_Details).Include(cas => cas.candidate_Application_Status.job).Include(cas => cas.candidate_Application_Status.application_Status).Include(cas => cas.candidate_Application_Status.candidate_Details.role).Include(cas => cas.candidate_Application_Status.job.job_Status).Include(cas => cas.candidate_Application_Status.job.user).FirstOrDefault(i => i.Interview_id == Interview_id);
            return responses;
        }

        public async Task<Interview> saveInterview(Interview interview)
        {
            Console.WriteLine(interview.Application_id);
            _context.Interview.Add(interview);
            _context.SaveChanges();
            return interview;
        }

        public async Task<Interview> updateInterview(Interview interview)
        {
            _context.Interview.Update(interview);
            _context.SaveChanges();
            return interview;
        }   
    }
}