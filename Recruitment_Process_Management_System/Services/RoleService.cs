using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<bool> deleteRole(int Role_id) {
            var role = await _roleRepository.getRoleById(Role_id);
            if(role == null)
            return false;
            return await _roleRepository.deleteRole(role);
        } 

        public async Task<IEnumerable<Role>> getAllRole() {
            return await _roleRepository.getAllRole();
        }

        public async Task<Role> getRoleById(int Role_id) {
            return await _roleRepository.getRoleById(Role_id);
        }

        public async Task<Role> saveRole(Role role) {
            Role newRole = new Role{
                Role_Name = role.Role_Name,
                Role_Description = role.Role_Description,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _roleRepository.saveRole(newRole);
        }

        public async Task<Role> updateRole(int Role_id, Role role) {
            return await _roleRepository.updateRole(Role_id,role);
        } 
    }
}