using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Services
{
    public interface IUserService
    {
        IEnumerable<User> getAllUser();
        User getUserById(int userId);
        User saveUser(UserDTO useDTO);
        User updateUser(int userId,UserDTO userDTO);
        bool deleteUser(int userId);
    }
}