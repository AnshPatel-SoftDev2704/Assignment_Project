using Recruitment_Process_Management_System.data;
using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool deleteRole(int Role_id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Role_id == Role_id);
            if(role == null)
            return false;
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Role> getAllRole()
        {
            var roles = _context.Roles.ToList();
            return roles;
        }

        public Role getRoleById(int Role_id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Role_id == Role_id);
            return role;
        }

        public Role saveRole(Role role)
        {
            var isRoleExist = _context.Roles.FirstOrDefault(r => r.Role_Name == role.Role_Name);
            if(isRoleExist != null)
            return null;

            Role newRole = new Role{
                Role_Name = role.Role_Name,
                Role_Description = role.Role_Description,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            _context.Roles.Add(newRole);
            _context.SaveChanges();
            return newRole;
        }

        public Role updateRole(int Role_id, Role role)
        {
            var existingRole = _context.Roles.FirstOrDefault(r => r.Role_Name == role.Role_Name);
            if(existingRole == null)
            return null;

            existingRole.Role_Description = role.Role_Description;
            existingRole.Role_Name = role.Role_Name;
            existingRole.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingRole;
        }
    }
}