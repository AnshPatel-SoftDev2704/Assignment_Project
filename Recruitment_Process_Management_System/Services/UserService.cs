using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool deleteUser(int userId) => _userRepository.deleteUser(userId);

        public IEnumerable<User> getAllUser() => _userRepository.getAllUser();

        public User getUserById(int userId) => _userRepository.getUserById(userId);

        public User saveUser(UserDTO userDTO)
        {
            return _userRepository.saveUser(userDTO);
        }

        public User updateUser(int userId, UserDTO userDTO) => _userRepository.updateUser(userId,userDTO);
    }
}