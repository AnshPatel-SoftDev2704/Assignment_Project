using Recruitment_Process_Management_System.Models;
using Recruitment_Process_Management_System.data;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Process_Management_System.Repositories
{
    public class Candidate_DetailsRepository : ICandidate_DetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public Candidate_DetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool deleteCandidate_Details(int Candidate_id)
        {
            var candidate = _context.Candidate_Details.FirstOrDefault(cd => cd.Candidate_id == Candidate_id);
            if(candidate == null)
            return false;
            
            _context.Candidate_Details.Remove(candidate);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Candidate_Details> getAllCandidate_Details()
        {
            var candidates = _context.Candidate_Details.Include(r => r.role).ToList();
            return candidates;
        }

        public Candidate_Details getCandidate_DetailsById(int Candidate_id)
        {
            var candidate = _context.Candidate_Details.Include(r => r.role).FirstOrDefault(cd => cd.Candidate_id == Candidate_id);
            return candidate;
        }

        public Candidate_Details saveCandidate_Details(Candidate_DetailsDTO candidate_DetailsDTO)
        {
            var candidate = _context.Candidate_Details.FirstOrDefault(cd => cd.Candidate_name == candidate_DetailsDTO.Candidate_name);
            if(candidate != null)
            return null;
            Console.WriteLine(candidate_DetailsDTO.Role_id);
            var responseRole = _context.Roles.FirstOrDefault(r => r.Role_id == candidate_DetailsDTO.Role_id);
            if(responseRole == null)
            return null;

            Candidate_Details newCandidate = new Candidate_Details{
                Candidate_name = candidate_DetailsDTO.Candidate_name,
                Candidate_DOB = candidate_DetailsDTO.Candidate_DOB,
                Candidate_email = candidate_DetailsDTO.Candidate_email,
                Candidate_password = candidate_DetailsDTO.Candidate_password,
                Candidate_Total_Work_Experience = candidate_DetailsDTO.Candidate_Total_Work_Experience,
                PhoneNo = candidate_DetailsDTO.PhoneNo,
                CV_Path = candidate_DetailsDTO.CV_Path,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Role_id = candidate_DetailsDTO.Role_id,
                role = responseRole
            };

            _context.Candidate_Details.Add(newCandidate);
            _context.SaveChanges();
            return newCandidate;
        }

        public Candidate_Details updateCandidate_Details(int Candidate_id, Candidate_DetailsDTO candidate_DetailsDTO)
        {
            var existingCandidate = _context.Candidate_Details.FirstOrDefault(cd => cd.Candidate_id == Candidate_id);
            if(existingCandidate == null)
            return null;

            var responseRole = _context.Roles.FirstOrDefault(r => r.Role_id == candidate_DetailsDTO.Role_id);
            if(responseRole == null)
            return null;

            existingCandidate.Candidate_name = candidate_DetailsDTO.Candidate_name;
            existingCandidate.Candidate_DOB = candidate_DetailsDTO.Candidate_DOB;
            existingCandidate.Candidate_email = candidate_DetailsDTO.Candidate_email;
            existingCandidate.Candidate_password = candidate_DetailsDTO.Candidate_password;
            existingCandidate.Candidate_Total_Work_Experience = candidate_DetailsDTO.Candidate_Total_Work_Experience;
            existingCandidate.PhoneNo = candidate_DetailsDTO.PhoneNo;
            existingCandidate.CV_Path = candidate_DetailsDTO.CV_Path;
            existingCandidate.Updated_at = DateTime.Now;
            existingCandidate.Role_id = candidate_DetailsDTO.Role_id;
            existingCandidate.role = responseRole;
            _context.SaveChanges();
            return existingCandidate;
        }
    }
}