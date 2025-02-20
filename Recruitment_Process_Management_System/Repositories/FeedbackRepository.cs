using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteFeedback(Feedback feedback)
        {
            _context.Feedback.Remove(feedback);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Feedback>> getAllFeedback()
        {
            var responses = _context.Feedback.Include(a => a.interview.candidate_Application_Status).Include(i => i.interview.interview_Status).Include(i => i.interview.interview_Type).Include(cas => cas.interview.candidate_Application_Status.candidate_Details).Include(cas => cas.interview.candidate_Application_Status.job).Include(cas => cas.interview.candidate_Application_Status.application_Status).Include(cas => cas.interview.candidate_Application_Status.candidate_Details.role).Include(cas => cas.interview.candidate_Application_Status.job.job_Status).Include(cas => cas.interview.candidate_Application_Status.job.user).Include(f => f.user).Include(f => f.skill).ToList();
            return responses;
        }

        public async Task<Feedback> getFeedbackById(int Feedback_id)
        {
            var response = _context.Feedback.Include(a => a.interview.candidate_Application_Status).Include(i => i.interview.interview_Status).Include(i => i.interview.interview_Type).Include(cas => cas.interview.candidate_Application_Status.candidate_Details).Include(cas => cas.interview.candidate_Application_Status.job).Include(cas => cas.interview.candidate_Application_Status.application_Status).Include(cas => cas.interview.candidate_Application_Status.candidate_Details.role).Include(cas => cas.interview.candidate_Application_Status.job.job_Status).Include(cas => cas.interview.candidate_Application_Status.job.user).Include(f => f.user).FirstOrDefault(f => f.Feedback_id == Feedback_id);
            return response;
        }

        public async Task<Feedback> saveFeedback(Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
            return feedback;
        }

        public async Task<Feedback> updateFeedback(Feedback feedback)
        {
            _context.Feedback.Update(feedback);
            _context.SaveChanges();
            return feedback;
        }
    }
}