using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface INotifications_UsersService
    {
        Task<IEnumerable<Notifications_Users>> getAllUsersNotifications();

        Task<Notifications_Users> getUserNotificationById(int Notifications_Users_id);

        Task<Notifications_Users> saveUserNotification(Notifications_UsersDTO notifications_UsersDTO);

        Task<Notifications_Users> updateUserNotification(int Notifications_Users_id,Notifications_UsersDTO notifications_UsersDTO);

        Task<bool> deleteUserNotification(int Notifications_Users_id);
    }
}