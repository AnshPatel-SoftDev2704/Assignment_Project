using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Candidate_Application_StatusRepository : ICandidate_Application_StatusRepository
    {
        private readonly ApplicationDbContext _context;

        public Candidate_Application_StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteCandidate_Application_Status(int Candidate_Application_Status_id)
        {
            var response = _context.Candidate_Application_Status.FirstOrDefault(cas => cas.Candidate_Application_Status_id == Candidate_Application_Status_id);
            if(response == null)
            return false;

            _context.Candidate_Application_Status.Remove(response);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Candidate_Application_Status> getAllCandidate_Application_Status()
        {
            var response = _context.Candidate_Application_Status.Include(c => c.candidate_Details).Include(j => j.job).Include(j => j.job.job_Status).Include(j => j.job.user).Include(a => a.application_Status).ToList();
            return response;
        }

        public Candidate_Application_Status getCandidate_Application_StatusById(int Candidate_Application_Status_id)
        {
            var response = _context.Candidate_Application_Status.Include(c => c.candidate_Details).Include(j => j.job).Include(j => j.job.job_Status).Include(j => j.job.user).Include(a => a.application_Status).FirstOrDefault(cas => cas.Candidate_Application_Status_id == Candidate_Application_Status_id);
            return response;
        }

        public Candidate_Application_Status saveCandidate_Application_Status(Candidate_Application_StatusDTO candidate_Application_StatusDTO)
        {
            var response = _context.Candidate_Application_Status.FirstOrDefault(cas => cas.Candidate_id == candidate_Application_StatusDTO.Candidate_id);
            if(response != null)
            return null;

            var candidate = _context.Candidate_Details.FirstOrDefault(c => c.Candidate_id == candidate_Application_StatusDTO.Candidate_id);
            if(candidate == null)
            return null;

            var newJob = _context.Jobs.FirstOrDefault(j => j.Job_id == candidate_Application_StatusDTO.Job_id);
            if(newJob == null)
            return null;

            var applicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_id == candidate_Application_StatusDTO.Application_Status_id);
            if(applicationStatus == null)
            return null;

            Candidate_Application_Status candidate_Application_Status = new Candidate_Application_Status{
                Candidate_id = candidate_Application_StatusDTO.Candidate_id,
                candidate_Details = candidate,
                Job_id = candidate_Application_StatusDTO.Job_id,
                job = newJob,
                Application_Status_id = candidate_Application_StatusDTO.Application_Status_id,
                application_Status = applicationStatus,
                Applied_Date = candidate_Application_StatusDTO.Applied_Date,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            _context.Candidate_Application_Status.Add(candidate_Application_Status);
            _context.SaveChanges();
            return candidate_Application_Status;
        }

        public Candidate_Application_Status updateCandidate_Application_Status(int Candidate_Application_Status_id, Candidate_Application_StatusDTO candidate_Application_StatusDTO)
        {
            var response = _context.Candidate_Application_Status.FirstOrDefault(cas => cas.Candidate_Application_Status_id == Candidate_Application_Status_id);
            if(response == null)
            return null;
            
            var candidate = _context.Candidate_Details.FirstOrDefault(c => c.Candidate_id == candidate_Application_StatusDTO.Candidate_id);
            if(candidate == null)
            return null;

            var newJob = _context.Jobs.FirstOrDefault(j => j.Job_id == candidate_Application_StatusDTO.Job_id);
            if(newJob == null)
            return null;

            var applicationStatus = _context.Application_Status.FirstOrDefault(a => a.Application_Status_id == candidate_Application_StatusDTO.Application_Status_id);
            if(applicationStatus == null)
            return null;


            response.Candidate_id = candidate_Application_StatusDTO.Candidate_id;
            response.candidate_Details = candidate;
            response.Job_id = candidate_Application_StatusDTO.Job_id;
            response.job = newJob;
            response.Application_Status_id = candidate_Application_StatusDTO.Application_Status_id;
            response.application_Status = applicationStatus;
            response.Applied_Date = candidate_Application_StatusDTO.Applied_Date;
            response.Updated_at = DateTime.Now;

            _context.SaveChanges();
            return response;
        }
    }
}