using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Application_StatusRepository : IApplication_StatusRepository
    {
        private readonly ApplicationDbContext _context;

        public Application_StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> deleteApplication_Status(Application_Status application_Status)
        {
            _context.Application_Status.Remove(application_Status);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Application_Status>> getAllApplication_Status()
        {
            var applicationStatuses = _context.Application_Status.ToList();
            return applicationStatuses;
        }

        public async Task<Application_Status> getApplication_StatusById(int Application_Status_id)
        {
            var applicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_id == Application_Status_id);
            return applicationStatus;
        }

        public async Task<Application_Status> saveApplication_Status(Application_Status application_Status)
        {
            var applicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_Name == application_Status.Application_Status_Name);
            if(applicationStatus != null)
            throw new Exception("Application Station is Already Present");

            application_Status.Created_at = DateTime.Now;
            application_Status.Updated_at = DateTime.Now;
            _context.Application_Status.Add(application_Status);
            _context.SaveChanges();
            return application_Status;
        }

        public async Task<Application_Status> updateApplication_Status(Application_Status application_Status)
        {
            _context.Application_Status.Update(application_Status);
            _context.SaveChanges();
            return application_Status;
        }
    }
}