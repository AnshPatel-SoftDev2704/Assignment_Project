using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
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
        public DbSet<Job_Status> Job_Status {get;set;}
        public DbSet<UserRoles> UserRoles{get;set;}
        public DbSet<Jobs> Jobs{get;set;}
        public DbSet<Required_Job_Skill> Required_Job_Skill{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}