using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> getAllRole();
        Role getRoleById(int Role_id);
        Role saveRole(Role role);
        Role updateRole(int Role_id,Role role);
        bool deleteRole(int Role_id);
    }   
}