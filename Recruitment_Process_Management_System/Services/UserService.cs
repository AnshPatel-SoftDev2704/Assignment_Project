using System.Threading.Tasks;
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

        public async Task<bool> deleteUser(int userId) {
            var user =await _userRepository.getUserById(userId);
            if(user == null)
            return false;
            return await _userRepository.deleteUser(user);
        }

        public async Task<IEnumerable<User>> getAllUser() {
            return await _userRepository.getAllUser();
        }

        public async Task<User> getUserById(int userId) {
            return await _userRepository.getUserById(userId);
        }

        public async Task<User> saveUser(UserDTO userDTO)
        {
            User user = new User
            {
                name = userDTO.name,
                email = userDTO.email,
                contact = userDTO.contact,
                password = userDTO.password,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _userRepository.updateUser(user,"Add");
        }

        public async Task<User> updateUser(int userId, UserDTO userDTO) {
            var existingUser = await _userRepository.getUserById(userId);
            if(existingUser == null)
            throw new Exception("User Not Found");
            existingUser.name = userDTO.name;
            existingUser.email = userDTO.email;
            existingUser.contact = userDTO.contact;
            existingUser.password = userDTO.password;
            existingUser.Updated_at = DateTime.Now;
            return await _userRepository.updateUser(existingUser,"update");
        }

        public async Task<User> getUser(string username)
        {
            var user = await  _userRepository.GetPassword(username);
            return user;
        }
    }
}