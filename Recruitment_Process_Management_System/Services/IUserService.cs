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
    }
}