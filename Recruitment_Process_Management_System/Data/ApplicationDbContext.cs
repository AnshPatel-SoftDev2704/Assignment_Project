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
        public DbSet<Preferred_Job_Skill> Preferred_Job_Skill{get;set;}
        public DbSet<Candidate_Details> Candidate_Details{get;set;}
        public DbSet<Candidate_Skills> Candidate_Skills{get;set;}
        public DbSet<Application_Status> Application_Status{get;set;}
        public DbSet<Candidate_Application_Status> Candidate_Application_Status{get;set;}
        public DbSet<Interview_Type> Interview_Type {get;set;}
        public DbSet<Interview_Status> Interview_Status{get;set;}
        public DbSet<Interview> Interview{get;set;}
        public DbSet<Interviewer> Interviewer{get;set;}
        public DbSet<Document_Type> Document_Type{get;set;}
        public DbSet<Document_Submitted> Document_Submitted{get;set;}
        public DbSet<Feedback> Feedback{get;set;}
        public DbSet<Selected_Candidate> Selected_Candidates{get;set;}
        public DbSet<Notifications_Users> Notifications_Users{get;set;}
        public DbSet<Notifications_Candidate> Notifications_Candidates{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}