using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Repositories
{
    public interface IUserRolesRepository
    {
        IEnumerable<UserRoles> getAllUserRoles();

        UserRoles getUserRolesById(int UserRolesId);
        UserRoles saveUserRoles(UserRolesDTO userRolesDTO);
        UserRoles updateUserRoles(int UserRolesId,UserRolesDTO userRolesDTO);
        bool deleteUserRoles(int UserRolesId);
    }
}