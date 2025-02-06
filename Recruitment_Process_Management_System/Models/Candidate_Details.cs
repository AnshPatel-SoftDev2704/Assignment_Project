using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Recruitment_Process_Management_System.Models;

namespace Recruitment_Process_Management_System.Models;

public class Candidate_Details
{
    [Key]
    public int Candidate_id{get;set;}
    [Required]
    public string Candidate_name{get;set;}
    [Required]
    public DateTime Candidate_DOB{get;set;}
    [Required]
    public string Candidate_email{get;set;}
    [Required]
    public string Candidate_password{get;set;}
    [Required]
    public int Candidate_Total_Work_Experience{get;set;}
    [Required]
    public int PhoneNo{get;set;}
    [Required]
    public string CV_Path{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]  
    public DateTime Updated_at{get;set;}
    [Required]
    public int Role_id{get;set;}
    [ForeignKey("Role_id")]
    public Role role{get;set;}
}