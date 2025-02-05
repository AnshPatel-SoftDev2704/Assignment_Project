using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> getAllUser();

        User getUserById(int userId);
        User saveUser(UserDTO userDTO);
        User updateUser(int userId,UserDTO userDTO);
        bool deleteUser(int userId);
        string GetPassword(string name);
    }
}