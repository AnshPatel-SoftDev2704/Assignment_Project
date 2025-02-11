using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserRolesRepository _userRolesRepository;

        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<User> _logger;
        public UserService(IUserRepository userRepository,IUserRolesRepository userRolesRepository,IRoleRepository roleRepository,ILogger<User> logger)
        {
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
            _roleRepository = roleRepository;
            _logger = logger;
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

    public async Task<bool> deleteUserRoles(int UserRolesId)
        {
            try{
                var userRole = await _userRolesRepository.getUserRolesByUserRoleId(UserRolesId);
                if(userRole == null)
                return false;
                _userRolesRepository.deleteUserRoles(userRole);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error while Deleting User Role");
                throw;
            }
        }

        public async Task<IEnumerable<UserRoles>> getAllUserRoles() {
            try{
                return await _userRolesRepository.getAllUserRoles();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error while Fetching User Roles");
                throw;
            }
        }

        public async Task<IEnumerable<UserRoles>> getUserRolesById(int User_id) {
            try{
                return await _userRolesRepository.getUserRolesById(User_id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error while Fetching User Role By Id");
                throw;
            }
        }

        public async Task<UserRoles> saveUserRoles(UserRolesDTO userRolesDTO) {
            try{
                var responseUser = await _userRepository.getUserById(userRolesDTO.User_id);
                var responseRole = await _roleRepository.getRoleById(userRolesDTO.Role_id);

                UserRoles userRole = new UserRoles{
                    User_id = userRolesDTO.User_id,
                    user = responseUser,
                    Role_id = userRolesDTO.Role_id,
                    role = responseRole,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now
                };
                return await _userRolesRepository.saveUserRoles(userRole);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error while Fetching User Role By Id");
                throw;
            }
        }

        public async Task<UserRoles> updateUserRoles(int UserRolesId, UserRolesDTO userRolesDTO) {
            try{
                var existingUserRole = await _userRolesRepository.getUserRolesByUserRoleId(UserRolesId);
                if(existingUserRole == null)
                return null;
                var responseUser = await _userRepository.getUserById(userRolesDTO.User_id);
                var responseRole = await _roleRepository.getRoleById(userRolesDTO.Role_id);
                existingUserRole.User_id = userRolesDTO.User_id;
                existingUserRole.user = responseUser;
                existingUserRole.Role_id = userRolesDTO.Role_id;
                existingUserRole.role = responseRole;
                existingUserRole.Updated_at = DateTime.Now;
                await _userRolesRepository.updateUserRoles(existingUserRole);
                return existingUserRole;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error while Fetching User Role By Id");
                throw;
            }
        }
    public async Task<IEnumerable<Role>> getAllRole() {
            return await _roleRepository.getAllRole();
        }

        public async Task<Role> getRoleById(int Role_id) {
            return await _roleRepository.getRoleById(Role_id);
        }

        public async Task<Role> saveRole(Role role) {
            Role newRole = new Role{
                Role_Name = role.Role_Name,
                Role_Description = role.Role_Description,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            return await _roleRepository.saveRole(newRole);
        }
    }
}
