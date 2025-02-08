using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> getAllUser();

        Task<User> getUserById(int userId);
        Task<User> saveUser(UserDTO userDTO);
        Task<User> updateUser(User user,string type);
        Task<bool> deleteUser(User user);
        Task<string> GetPassword(string name);
    }
}