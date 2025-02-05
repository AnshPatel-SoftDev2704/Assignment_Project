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

        public bool deleteUserRoles(int UserRolesId)
        {
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserRoleId == UserRolesId);
            if(userRole == null)
            return false;

            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<UserRoles> getAllUserRoles()
        {
            var userRoles = _context.UserRoles.Include(u => u.user).Include(r=>r.role).ToList();
            return userRoles;
        }

        public UserRoles getUserRolesById(int UserRolesId)
        {
            var userRole = _context.UserRoles.Include(u => u.user).Include(r=>r.role).FirstOrDefault(ur => ur.UserRoleId == UserRolesId);
            return userRole;
        }

        public UserRoles saveUserRoles(UserRolesDTO userRolesDTO)
        {
            var responseUser = _context.Users.FirstOrDefault(u => u.User_id == userRolesDTO.User_id);
            var responseRole = _context.Roles.FirstOrDefault(r => r.Role_id == userRolesDTO.Role_id);

            UserRoles userRole = new UserRoles{
                User_id = userRolesDTO.User_id,
                user = responseUser,
                Role_id = userRolesDTO.Role_id,
                role = responseRole,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            return userRole;
        }

        public UserRoles updateUserRoles(int UserRolesId, UserRolesDTO userRolesDTO)
        {
            var existingUserRole = _context.UserRoles.FirstOrDefault(ur => ur.UserRoleId == UserRolesId);
            if(existingUserRole == null)
            return null;
            var responseUser = _context.Users.FirstOrDefault(u => u.User_id == userRolesDTO.User_id);
            var responseRole = _context.Roles.FirstOrDefault(r => r.Role_id == userRolesDTO.Role_id);
            existingUserRole.User_id = userRolesDTO.User_id;
            existingUserRole.user = responseUser;
            existingUserRole.Role_id = userRolesDTO.Role_id;
            existingUserRole.role = responseRole;
            existingUserRole.Updated_at = DateTime.Now;
            return existingUserRole;
        }
    }
}