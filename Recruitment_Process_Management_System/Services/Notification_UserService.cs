using System.Reflection.Metadata.Ecma335;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class Notifications_UserService : INotifications_UsersService
    {
        private readonly INotifications_UsersRepository _notifications_UsersRepository;

        private readonly IUserRepository _userRepository;

        public Notifications_UserService(INotifications_UsersRepository notifications_UsersRepository,
        IUserRepository userRepository)
        {
            _notifications_UsersRepository = notifications_UsersRepository;
            _userRepository = userRepository;
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
    }
}