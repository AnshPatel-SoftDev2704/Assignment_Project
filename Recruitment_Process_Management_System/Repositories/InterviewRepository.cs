using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class InterviewReposiory : IInterviewRepository
    {
        private readonly ApplicationDbContext _context;

        public InterviewReposiory(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteInterview(int Interview_id)
        {
            var response = _context.Interview.FirstOrDefault(i => i.Interview_id == Interview_id);
            if(response == null)
            return false;
            
            _context.Interview.Remove(response);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Interview> getAllInterview()
        {
            var responses = _context.Interview.Include(a => a.candidate_Application_Status).Include(i => i.interview_Status).Include(i => i.interview_Type).Include(cas => cas.candidate_Application_Status.candidate_Details).Include(cas => cas.candidate_Application_Status.job).Include(cas => cas.candidate_Application_Status.application_Status).Include(cas => cas.candidate_Application_Status.candidate_Details.role).Include(cas => cas.candidate_Application_Status.job.job_Status).Include(cas => cas.candidate_Application_Status.job.user).ToList();
            return responses;
        }

        public Interview getInterviewById(int Interview_id)
        {
            var responses = _context.Interview.Include(a => a.candidate_Application_Status).Include(i => i.interview_Status).Include(i => i.interview_Type).Include(cas => cas.candidate_Application_Status.candidate_Details).Include(cas => cas.candidate_Application_Status.job).Include(cas => cas.candidate_Application_Status.application_Status).Include(cas => cas.candidate_Application_Status.candidate_Details.role).Include(cas => cas.candidate_Application_Status.job.job_Status).Include(cas => cas.candidate_Application_Status.job.user).FirstOrDefault(i => i.Interview_id == Interview_id);
            return responses;
        }

        public Interview saveInterview(InterviewDTO interviewDTO)
        {
            var newCandidate_Application_Status = _context.Candidate_Application_Status.FirstOrDefault(cas => cas.Candidate_Application_Status_id == interviewDTO.Application_id);
            var newInterview_Type = _context.Interview_Type.FirstOrDefault(it => it.Interview_Type_id == interviewDTO.Interview_Type_id);
            var newInterview_Status = _context.Interview_Status.FirstOrDefault(i => i.Interview_Status_id == interviewDTO.Interview_Status_id);

            Interview interview = new Interview{
                Application_id = interviewDTO.Application_id,
                candidate_Application_Status = newCandidate_Application_Status,
                Number_of_round = interviewDTO.Number_of_round,
                Interview_Type_id = interviewDTO.Interview_Type_id,
                interview_Type = newInterview_Type,
                Scheduled_at = interviewDTO.Scheduled_at,
                Interview_Status_id = interviewDTO.Interview_Status_id,
                interview_Status = newInterview_Status,
                Special_Notes = interviewDTO.Special_Notes,
                Interview_Meeting_Link = interviewDTO.Interview_Meeting_Link,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            _context.Interview.Add(interview);
            _context.SaveChanges();
            return interview;
        }

        public Interview updateInterview(int Interview_id, InterviewDTO interviewDTO)
        {
            var newCandidate_Application_Status = _context.Candidate_Application_Status.FirstOrDefault(cas => cas.Candidate_Application_Status_id == interviewDTO.Application_id);
            var newInterview_Type = _context.Interview_Type.FirstOrDefault(it => it.Interview_Type_id == interviewDTO.Interview_Type_id);
            var newInterview_Status = _context.Interview_Status.FirstOrDefault(i => i.Interview_Status_id == interviewDTO.Interview_Status_id);
            var existingInterview = _context.Interview.FirstOrDefault(i => i.Interview_id == Interview_id);

            if(existingInterview == null)
            return null;

            existingInterview.Application_id = interviewDTO.Application_id;
            existingInterview.candidate_Application_Status = newCandidate_Application_Status;
            existingInterview.Number_of_round = interviewDTO.Number_of_round;
            existingInterview.Interview_Type_id = interviewDTO.Interview_Type_id;
            existingInterview.interview_Type = newInterview_Type;
            existingInterview.Scheduled_at = interviewDTO.Scheduled_at;
            existingInterview.Interview_Status_id = interviewDTO.Interview_Status_id;
            existingInterview.interview_Status = newInterview_Status;
            existingInterview.Special_Notes = interviewDTO.Special_Notes;
            existingInterview.Interview_Meeting_Link = interviewDTO.Interview_Meeting_Link;
            existingInterview.Updated_at = DateTime.Now;

            _context.SaveChanges();
            return existingInterview;
        }   
    }
}