using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Notification_CandidateRepository : INotification_CandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public Notification_CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteCandidateNotification(Notifications_Candidate notifications_Candidate)
        {
            _context.Notifications_Candidates.Remove(notifications_Candidate);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Notifications_Candidate>> getAllCandidateNotifications()
        {
            var responses = _context.Notifications_Candidates.Include(nc => nc.candidate).Include(nc => nc.candidate.role).ToList();
            return responses;
        }

        public async Task<Notifications_Candidate> getCandidateNotificationById(int Notifications_Candidate_id)
        {
            var response = _context.Notifications_Candidates.Include(nc => nc.candidate).Include(nc => nc.candidate.role).FirstOrDefault(nu => nu.Notifications_Candidates_id == Notifications_Candidate_id);
            return response;
        }

        public async Task<Notifications_Candidate> saveCandidateNotification(Notifications_Candidate notifications_Candidate)
        {
            _context.Notifications_Candidates.Add(notifications_Candidate);
            _context.SaveChanges();
            return notifications_Candidate;
        }

        public async Task<Notifications_Candidate> updateCandidateNotification(Notifications_Candidate notifications_Candidate)
        {
            _context.Notifications_Candidates.Update(notifications_Candidate);
            _context.SaveChanges();
            return notifications_Candidate;
        }
    }
}