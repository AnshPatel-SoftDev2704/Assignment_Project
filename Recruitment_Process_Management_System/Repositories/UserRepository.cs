using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.User_id == userId);
            if(user == null)
            return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<User> getAllUser()
        {
            var users = _context.Users.Include(u => u.Role).ToList();
            return users;
        }

        public User getUserById(int userId)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.User_id == userId);
            return user;
        }

        public User saveUser(UserDTO userDTO)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.name == userDTO.name);
            var role = _context.Roles.FirstOrDefault(r => r.Role_id == userDTO.Role_id);
            if(existingUser != null)
                return null;
            Console.WriteLine(userDTO.Role_id);
            User user = new User
            {
                name = userDTO.name,
                Role_id = userDTO.Role_id,
                Role = role,
                email = userDTO.email,
                contact = userDTO.contact,
                password = userDTO.password,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };   
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User updateUser(int userId, UserDTO userDTO)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.User_id == userId);
            var role = _context.Roles.FirstOrDefault(r => r.Role_id == userDTO.Role_id);
            if(existingUser == null)
            return null;

            existingUser.name = userDTO.name;
            existingUser.Role_id = userDTO.Role_id;
            existingUser.Role = role;
            existingUser.email = userDTO.email;
            existingUser.contact = userDTO.contact;
            existingUser.password = userDTO.password;
            existingUser.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingUser;
        }

        public string GetPassword(string name)
        {
            var user = _context.Users.FirstOrDefault(u => u.name == name);
            return user.password;
        }
    }   
}