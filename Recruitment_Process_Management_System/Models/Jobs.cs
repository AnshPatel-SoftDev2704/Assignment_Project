using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Jobs
{
    [Key]
    public int Job_id{get;set;}
    [Required]
    public string Job_title{get;set;}
    [Required]
    public string Job_description{get;set;}
    [Required]
    public int Job_Status_id{get;set;}
    [ForeignKey("Job_Status_id")]
    public Job_Status job_Status{get;set;}
    [Required]
    public string Job_Closed_reason{get;set;}

    [Required]
    public int Job_Selected_Candidate_id{get;set;}
    [Required]
    public int Created_by{get;set;}
    [ForeignKey("Created_by")]
    public User user{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}