using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Notifications_UsersRepository : INotifications_UsersRepository
    {
        private readonly ApplicationDbContext _context;

        public Notifications_UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteUserNotification(Notifications_Users notifications_Users)
        {
            _context.Notifications_Users.Remove(notifications_Users);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Notifications_Users>> getAllUserNotifications()
        {
            var responses = _context.Notifications_Users.Include(nu => nu.user).ToList();
            return responses;
        }

        public async Task<Notifications_Users> getUserNotificationById(int Notifications_Users_id)
        {
            var response = _context.Notifications_Users.FirstOrDefault(nu => nu.Notifications_Users_id == Notifications_Users_id);
            return response;
        }

        public async Task<Notifications_Users> saveUserNotification(Notifications_Users notifications_Users)
        {
            _context.Notifications_Users.Add(notifications_Users);
            _context.SaveChanges();
            return notifications_Users;
        }

        public async Task<Notifications_Users> updateUserNotification(Notifications_Users notifications_Users)
        {
            _context.Notifications_Users.Update(notifications_Users);
            _context.SaveChanges();
            return notifications_Users;
        }
    }
}