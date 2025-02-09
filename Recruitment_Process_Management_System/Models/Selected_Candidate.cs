using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Selected_Candidate
{
    [Key]
    public int Selected_Candidate_id{get;set;}
    [Required]
    public int Candidate_id{get;set;}
    [ForeignKey("Candidate_id")]
    public Candidate_Details candidate_Details{get;set;}
    [Required]
    public int Job_id{get;set;}
    [ForeignKey("Job_id")]
    public Jobs job{get;set;}
    [Required]
    public DateTime Joining_date{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}