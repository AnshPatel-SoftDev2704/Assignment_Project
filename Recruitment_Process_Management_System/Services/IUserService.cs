using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> getAllUser();
        Task<User> getUserById(int userId);
        Task<User> saveUser(UserDTO useDTO);
        Task<User> updateUser(int userId,UserDTO userDTO);
        Task<bool> deleteUser(int userId);
        Task<User> getUser(string username);
        Task<IEnumerable<UserRoles>> getAllUserRoles();
        Task<IEnumerable<UserRoles>> getUserRolesById(int UserRolesId);
        Task<UserRoles> saveUserRoles(UserRolesDTO userRolesDTO);
        Task<UserRoles> updateUserRoles(int UserRolesId,UserRolesDTO userRolesDTO);
        Task<bool> deleteUserRoles(int UserRolesId);
        Task<IEnumerable<Role>> getAllRole();
        Task<Role> getRoleById(int Role_id);
        Task<Role> saveRole(Role role);
    }
}