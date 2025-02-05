using Microsoft.EntityFrameworkCore;
using Recruitment_Process_Management_System.Models;
namespace Recruitment_Process_Management_System.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users {get;set;}
        public DbSet<Role> Roles {get;set;}
        public DbSet<Skill> Skills {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}