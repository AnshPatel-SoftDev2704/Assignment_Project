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
        public bool deleteRole(int Role_id) => _roleRepository.deleteRole(Role_id);

        public IEnumerable<Role> getAllRole() => _roleRepository.getAllRole();

        public Role getRoleById(int Role_id) => _roleRepository.getRoleById(Role_id);

        public Role saveRole(Role role) => _roleRepository.saveRole(role);

        public Role updateRole(int Role_id, Role role) => _roleRepository.updateRole(Role_id,role);
    }
}