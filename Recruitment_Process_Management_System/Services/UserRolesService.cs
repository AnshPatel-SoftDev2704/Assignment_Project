using System.Threading.Tasks;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<UserRolesService> _logger;

        public UserRolesService(IUserRolesRepository userRolesRepository,
        IUserRepository userRepository, IRoleRepository roleRepository,ILogger<UserRolesService> logger)
        {
            _userRolesRepository = userRolesRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _logger = logger;
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
    }
}