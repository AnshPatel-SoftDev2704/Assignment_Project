using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Recruitment_Process_Management_System.Repositories
{
    public class InterviewerRepository : IInterviewerRepository
    {
        private readonly ApplicationDbContext _context;
        
        public InterviewerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteInterviewer(int Interviewer_id)
        {
            var response = _context.Interviewer.FirstOrDefault(i => i.Interviewer_id == Interviewer_id);
            if(response == null)
            return false;

            _context.Interviewer.Remove(response);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Interviewer> getAllInterviewer()
        {
            var responses = _context.Interviewer.Include(i => i.interview).Include(a => a.interview.candidate_Application_Status).Include(i => i.interview.interview_Status).Include(i => i.interview.interview_Type).Include(cas => cas.interview.candidate_Application_Status.candidate_Details).Include(cas => cas.interview.candidate_Application_Status.job).Include(cas => cas.interview.candidate_Application_Status.application_Status).Include(cas => cas.interview.candidate_Application_Status.candidate_Details.role).Include(cas => cas.interview.candidate_Application_Status.job.job_Status).Include(cas => cas.interview.candidate_Application_Status.job.user).Include(i => i.user).ToList();
            return responses;
        }

        public Interviewer getInterviewerById(int Interviewer_id)
        {
            var response = _context.Interviewer.Include(i => i.interview).Include(a => a.interview.candidate_Application_Status).Include(i => i.interview.interview_Status).Include(i => i.interview.interview_Type).Include(cas => cas.interview.candidate_Application_Status.candidate_Details).Include(cas => cas.interview.candidate_Application_Status.job).Include(cas => cas.interview.candidate_Application_Status.application_Status).Include(cas => cas.interview.candidate_Application_Status.candidate_Details.role).Include(cas => cas.interview.candidate_Application_Status.job.job_Status).Include(cas => cas.interview.candidate_Application_Status.job.user).Include(i => i.user).FirstOrDefault(i => i.Interviewer_id == Interviewer_id);
            return response;
        }

        public Interviewer saveInterviewer(InterviewerDTO interviewerDTO)
        {
            var newInterview = _context.Interview.FirstOrDefault(i => i.Interview_id == interviewerDTO.Interview_id);
            var newUser = _context.Users.FirstOrDefault(u => u.User_id == interviewerDTO.User_id);

            Interviewer interviewer = new Interviewer{
                Interview_id = interviewerDTO.Interview_id,
                interview = newInterview,
                User_id = interviewerDTO.User_id,
                user = newUser,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            _context.Interviewer.Add(interviewer);
            _context.SaveChanges();
            return interviewer;
        }

        public Interviewer updateInterviewer(int Interviewer_id, InterviewerDTO interviewerDTO)
        {
            var newInterview = _context.Interview.FirstOrDefault(i => i.Interview_id == interviewerDTO.Interview_id);
            var newUser = _context.Users.FirstOrDefault(u => u.User_id == interviewerDTO.User_id);
            var existingInterviewer = _context.Interviewer.FirstOrDefault(i => i.Interviewer_id == Interviewer_id);

            if(existingInterviewer == null)
            return null;

            existingInterviewer.Interview_id = interviewerDTO.Interview_id;
            existingInterviewer.interview = newInterview;
            existingInterviewer.User_id = interviewerDTO.User_id;
            existingInterviewer.user = newUser;
            existingInterviewer.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingInterviewer;
        }
    }
}