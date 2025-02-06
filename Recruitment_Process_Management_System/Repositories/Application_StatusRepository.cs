using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.VisualBasic;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Application_StatusRepository : IApplication_StatusRepository
    {
        private readonly ApplicationDbContext _context;

        public Application_StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteApplication_Status(int Application_Status_id)
        {
            var applicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_id == Application_Status_id);
            if(applicationStatus == null)
            return false;

            _context.Application_Status.Remove(applicationStatus);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Application_Status> getAllApplication_Status()
        {
            var applicationStatuses = _context.Application_Status.ToList();
            return applicationStatuses;
        }

        public Application_Status getApplication_StatusById(int Application_Status_id)
        {
            var applicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_id == Application_Status_id);
            return applicationStatus;
        }

        public Application_Status saveApplication_Status(Application_Status application_Status)
        {
            var applicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_Name == application_Status.Application_Status_Name);
            if(applicationStatus != null)
            return null;

            application_Status.Created_at = DateTime.Now;
            application_Status.Updated_at = DateTime.Now;
            _context.Application_Status.Add(application_Status);
            _context.SaveChanges();
            return application_Status;
        }

        public Application_Status updateApplication_Status(int Application_Status_id, Application_Status application_Status)
        {
            var existingApplicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_id == Application_Status_id);
            if(application_Status == null)
            return null;

            existingApplicationStatus.Application_Status_Name = application_Status.Application_Status_Name;
            existingApplicationStatus.Application_Status_Description = application_Status.Application_Status_Description;
            existingApplicationStatus.Updated_at = DateTime.Now;
            _context.SaveChanges();
            return existingApplicationStatus;
        }
    }
}