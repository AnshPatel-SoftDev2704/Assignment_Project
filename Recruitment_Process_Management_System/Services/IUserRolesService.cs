using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IUserRolesService
    {
        Task<IEnumerable<UserRoles>> getAllUserRoles();
        Task<UserRoles> getUserRolesById(int UserRolesId);
        Task<UserRoles> saveUserRoles(UserRolesDTO userRolesDTO);
        Task<UserRoles> updateUserRoles(int UserRolesId,UserRolesDTO userRolesDTO);
        Task<bool> deleteUserRoles(int UserRolesId);
    }
}