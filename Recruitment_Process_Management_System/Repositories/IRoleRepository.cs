using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> getAllRole();
        Task<Role> getRoleById(int Role_id);
        Task<Role> saveRole(Role role);
        Task<Role> updateRole(int Role_id,Role role);
        Task<bool> deleteRole(Role role);
    }   
}