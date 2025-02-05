using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public UserRolesService(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        public bool deleteUserRoles(int UserRolesId) => _userRolesRepository.deleteUserRoles(UserRolesId);

        public IEnumerable<UserRoles> getAllUserRoles() => _userRolesRepository.getAllUserRoles();

        public UserRoles getUserRolesById(int UserRolesId) => _userRolesRepository.getUserRolesById(UserRolesId);

        public UserRoles saveUserRoles(UserRolesDTO userRolesDTO) => _userRolesRepository.saveUserRoles(userRolesDTO);

        public UserRoles updateUserRoles(int UserRolesId, UserRolesDTO userRolesDTO) => _userRolesRepository.updateUserRoles(UserRolesId,userRolesDTO);
    }
}