using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteUserRoles(UserRoles userRole)
        {
            try{
                _context.UserRoles.Remove(userRole);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserRoles>> getAllUserRoles()
        {
            try{
                var userRoles = _context.UserRoles.Include(u => u.user).Include(r=>r.role).ToList();
                return userRoles;
            }
            catch(Exception ex)
            {
                throw;
            } 
        }

        public async Task<UserRoles> getUserRolesById(int UserRolesId)
        {
            try{
                var userRole = _context.UserRoles.Include(u => u.user).Include(r=>r.role).FirstOrDefault(ur => ur.UserRoleId == UserRolesId);
                return userRole;
            }
            catch(Exception ex)
            {
                throw;
            }    
        }

        public async Task<UserRoles> saveUserRoles(UserRoles userRole)
        {
            try{
                _context.UserRoles.Add(userRole);
                _context.SaveChanges();
                return userRole;
            }
            catch(Exception ex)
            {
                throw;
            }      
        }

        public async Task<UserRoles> updateUserRoles(UserRoles userRole)
        {
            try{
                _context.UserRoles.Update(userRole);
                _context.SaveChanges();
                return userRole;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}