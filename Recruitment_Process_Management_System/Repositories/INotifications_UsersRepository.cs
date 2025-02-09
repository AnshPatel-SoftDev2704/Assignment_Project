using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface INotifications_UsersRepository
    {
        Task<IEnumerable<Notifications_Users>> getAllUserNotifications();

        Task<Notifications_Users> getUserNotificationById(int Notifications_Users_id);

        Task<Notifications_Users> saveUserNotification(Notifications_Users notifications_Users);

        Task<Notifications_Users> updateUserNotification(Notifications_Users notifications_Users);

        Task<bool> deleteUserNotification(Notifications_Users notifications_Users);
    }
}