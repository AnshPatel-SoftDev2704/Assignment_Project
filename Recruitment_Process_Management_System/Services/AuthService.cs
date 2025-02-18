using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class AuthService(IConfiguration configuration, IUserRepository userRepository,IUserRolesRepository userRolesRepository,ICandidate_DetailsRepository candidate_DetailsRepository)
    {
        private readonly IConfiguration _configuration = configuration;

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICandidate_DetailsRepository _candidate_DetailsRepository = candidate_DetailsRepository;

        private readonly IUserRolesRepository _userRolesRepository = userRolesRepository;
        public async Task<string> Authenticate(string Name,string Password)
        {
            var response = await _userRepository.GetPassword(Name);
            Candidate_Details candidate;

            if(response == null)
            {
                candidate = await _candidate_DetailsRepository.GetCandidate_DetailsByName(Name);
                Console.WriteLine(candidate.Candidate_password);
                if(Password == candidate.Candidate_password)
                {
                    return GenerateCandidateJwtToken(Name,Password,"Candidate");
                }
                else
                return "";
            }
            else{
                List<UserRoles> roles = (await _userRolesRepository.getUserRolesById(response.User_id)).ToList();
            if(Password == response.password)
            {
                return GenerateJwtToken(Name,Password,roles);
            }
            }
            return "";
            
        }

        public string GenerateJwtToken(string userName, string Password,List<UserRoles> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.GivenName, Password)
            };

            foreach(var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role,role.role.Role_Name));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);

            return userToken;
        }
        public string GenerateCandidateJwtToken(string userName, string Password,string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.GivenName, Password),
                new Claim(ClaimTypes.Role,role)
            };

            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);

            return userToken;
        }
    }
}