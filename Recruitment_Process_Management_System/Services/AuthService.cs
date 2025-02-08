using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Recruitment_Process_Management_System.Repositories;

namespace Recruitment_Process_Management_System.Services
{
    public class AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        private readonly IConfiguration _configuration = configuration;

        private readonly IUserRepository _userRepository = userRepository;

        public string Authenticate(string Name,string Password)
        {
            var response = _userRepository.GetPassword(Name);
            var password = response.ToString();
            if(Password == password)
            {
                return GenerateJwtToken(Name,Password,"admin");
            }
            else
            return "";
        }

        public string GenerateJwtToken(string userName, string Password, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.GivenName, Password),
                    new Claim(ClaimTypes.Role, role)
                }),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);

            return userToken;
        }
    }
}