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

        public async Task<bool> deleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<User>> getAllUser()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public async Task<User> getUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.User_id == userId);
            return user;
        }

        public async Task<User> saveUser(UserDTO userDTO)
        {
            // var existingUser = _context.Users.FirstOrDefault(u => u.name == userDTO.name);
            // if(existingUser != null)
            //     return null;
            User user = new User
            {
                name = userDTO.name,
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

        public async Task<User> updateUser(User user,string type)
        {
            if(type == "Add")
            _context.Users.Add(user);
            else
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> GetPassword(string name)
        {
            User user = _context.Users.FirstOrDefault(u => u.name == name);
            return user;
        }
    }   
}