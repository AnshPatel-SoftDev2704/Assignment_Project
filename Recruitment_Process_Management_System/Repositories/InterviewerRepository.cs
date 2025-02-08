using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class InterviewerRepository : IInterviewerRepository
    {
        private readonly ApplicationDbContext _context;
        
        public InterviewerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteInterviewer(Interviewer interviewer)
        {
            _context.Interviewer.Remove(interviewer);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Interviewer>> getAllInterviewer()
        {
            var responses = _context.Interviewer.Include(i => i.interview).Include(a => a.interview.candidate_Application_Status).Include(i => i.interview.interview_Status).Include(i => i.interview.interview_Type).Include(cas => cas.interview.candidate_Application_Status.candidate_Details).Include(cas => cas.interview.candidate_Application_Status.job).Include(cas => cas.interview.candidate_Application_Status.application_Status).Include(cas => cas.interview.candidate_Application_Status.candidate_Details.role).Include(cas => cas.interview.candidate_Application_Status.job.job_Status).Include(cas => cas.interview.candidate_Application_Status.job.user).Include(i => i.user).ToList();
            return responses;
        }

        public async Task<Interviewer> getInterviewerById(int Interviewer_id)
        {
            var response = _context.Interviewer.Include(i => i.interview).Include(a => a.interview.candidate_Application_Status).Include(i => i.interview.interview_Status).Include(i => i.interview.interview_Type).Include(cas => cas.interview.candidate_Application_Status.candidate_Details).Include(cas => cas.interview.candidate_Application_Status.job).Include(cas => cas.interview.candidate_Application_Status.application_Status).Include(cas => cas.interview.candidate_Application_Status.candidate_Details.role).Include(cas => cas.interview.candidate_Application_Status.job.job_Status).Include(cas => cas.interview.candidate_Application_Status.job.user).Include(i => i.user).FirstOrDefault(i => i.Interviewer_id == Interviewer_id);
            return response;
        }

        public async Task<Interviewer> saveInterviewer(Interviewer interviewer)
        {
            _context.Interviewer.Add(interviewer);
            _context.SaveChanges();
            return interviewer;
        }

        public async Task<Interviewer> updateInterviewer(Interviewer interviewer)
        {
            _context.Interviewer.Update(interviewer);
            _context.SaveChanges();
            return interviewer;
        }
    }
}