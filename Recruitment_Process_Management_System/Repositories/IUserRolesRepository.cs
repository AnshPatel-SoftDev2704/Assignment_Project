using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IUserRolesRepository
    {
        Task<IEnumerable<UserRoles>> getAllUserRoles();

        Task<IEnumerable<UserRoles>> getUserRolesById(int UserRolesId);
        Task<UserRoles> saveUserRoles(UserRoles userRole);
        Task<UserRoles> updateUserRoles(UserRoles userRole);
        Task<bool> deleteUserRoles(UserRoles userRole);
        Task<UserRoles> getUserRolesByUserRoleId(int UserRolesId);
    }
}