using System.Reflection.Metadata.Ecma335;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Notification_CandidateService : INotification_CandidateService
    {
        private readonly INotification_CandidateRepository _notification_CandidateRepository;

        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;

        public Notification_CandidateService(INotification_CandidateRepository notification_CandidateRepository,
        ICandidate_DetailsRepository candidate_DetailsRepository)
        {
            _notification_CandidateRepository = notification_CandidateRepository;
            _candidate_DetailsRepository = candidate_DetailsRepository;
        }

        public async Task<bool> deleteCandidateNotification(int Notifications_Candidate_id)
        {
            var response = await _notification_CandidateRepository.getCandidateNotificationById(Notifications_Candidate_id);
            if(response == null)
            throw new Exception("Candidate Notification Not Found");

            return await _notification_CandidateRepository.deleteCandidateNotification(response);
        }

        public async Task<IEnumerable<Notifications_Candidate>> getAllCandidateNotifications() => await _notification_CandidateRepository.getAllCandidateNotifications();

        public async Task<Notifications_Candidate> getCandidateNotificationById(int Notifications_Candidate_id) => await _notification_CandidateRepository.getCandidateNotificationById(Notifications_Candidate_id);

        public async Task<Notifications_Candidate> saveCandidateNotification(Notifications_CandidateDTO notifications_CandidateDTO)
        {
            var newCandidate = await _candidate_DetailsRepository.getCandidate_DetailsById(notifications_CandidateDTO.Candidate_id);
            if(newCandidate == null)
            throw new Exception("Candidate Not Found");

            Notifications_Candidate notifications_Candidate = new Notifications_Candidate{
                Candidate_id = notifications_CandidateDTO.Candidate_id,
                candidate = newCandidate,
                Message = notifications_CandidateDTO.Message,
                Status = false,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            return await _notification_CandidateRepository.saveCandidateNotification(notifications_Candidate);
        }

        public async Task<Notifications_Candidate> updateCandidateNotification(int Notifications_Candidate_id, Notifications_CandidateDTO notifications_CandidateDTO)
        {
            var newCandidate = await _candidate_DetailsRepository.getCandidate_DetailsById(notifications_CandidateDTO.Candidate_id);
            if(newCandidate == null)
            throw new Exception("Candidate Not Found");

            var existingCandidateNotification = await _notification_CandidateRepository.getCandidateNotificationById(Notifications_Candidate_id);
            if(existingCandidateNotification == null)
            throw new Exception("Candidate Notification Not Found");

            existingCandidateNotification.Candidate_id = notifications_CandidateDTO.Candidate_id;
            existingCandidateNotification.candidate = newCandidate;
            existingCandidateNotification.Message = notifications_CandidateDTO.Message;
            existingCandidateNotification.Status = notifications_CandidateDTO.Status;
            existingCandidateNotification.Updated_at = DateTime.Now;

            return await _notification_CandidateRepository.updateCandidateNotification(existingCandidateNotification);
        }
    }
}