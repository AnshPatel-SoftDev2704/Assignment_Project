using System.Reflection.Metadata.Ecma335;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Notifications_UserService : INotifications_UsersService
    {
        private readonly INotifications_UsersRepository _notifications_UsersRepository;

        private readonly IUserRepository _userRepository;

        private readonly INotification_CandidateRepository _notification_CandidateRepository;

        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository;


        public Notifications_UserService(INotifications_UsersRepository notifications_UsersRepository,
        IUserRepository userRepository,INotification_CandidateRepository notification_CandidateRepository,
        ICandidate_DetailsRepository candidate_DetailsRepository)
        {
            _notifications_UsersRepository = notifications_UsersRepository;
            _userRepository = userRepository;
            _notification_CandidateRepository = notification_CandidateRepository;
            _candidate_DetailsRepository = candidate_DetailsRepository;
        }

        public async Task<bool> deleteUserNotification(int Notifications_Users_id)
        {
            var response = await _notifications_UsersRepository.getUserNotificationById(Notifications_Users_id);
            if(response == null)
            throw new Exception("User Notification Not Found");

            return await _notifications_UsersRepository.deleteUserNotification(response);
        }

        public async Task<IEnumerable<Notifications_Users>> getAllUsersNotifications() => await _notifications_UsersRepository.getAllUserNotifications();

        public async Task<Notifications_Users> getUserNotificationById(int Notifications_Users_id) => await _notifications_UsersRepository.getUserNotificationById(Notifications_Users_id);

        public async Task<Notifications_Users> saveUserNotification(Notifications_UsersDTO notifications_UsersDTO)
        {
            var newUser = await _userRepository.getUserById(notifications_UsersDTO.User_id);
            if(newUser == null)
            throw new Exception("User Not Found");

            Notifications_Users notifications_Users = new Notifications_Users{
                User_id = notifications_UsersDTO.User_id,
                user = newUser,
                Message = notifications_UsersDTO.Message,
                Status = false,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            return await _notifications_UsersRepository.saveUserNotification(notifications_Users);
        }

        public async Task<Notifications_Users> updateUserNotification(int Notifications_Users_id, Notifications_UsersDTO notifications_UsersDTO)
        {
            var newUser = await _userRepository.getUserById(notifications_UsersDTO.User_id);
            if(newUser == null)
            throw new Exception("User Not Found");

            var existingUserNotification = await _notifications_UsersRepository.getUserNotificationById(Notifications_Users_id);
            if(existingUserNotification == null)
            throw new Exception("User Notification Not Found");

            existingUserNotification.User_id = notifications_UsersDTO.User_id;
            existingUserNotification.user = newUser;
            existingUserNotification.Message = notifications_UsersDTO.Message;
            existingUserNotification.Status = notifications_UsersDTO.Status;
            existingUserNotification.Updated_at = DateTime.Now;

            return await _notifications_UsersRepository.updateUserNotification(existingUserNotification);
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