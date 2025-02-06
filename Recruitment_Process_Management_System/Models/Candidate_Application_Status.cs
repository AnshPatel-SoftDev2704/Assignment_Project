using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace Recruitment_Process_Management_System.Models;

public class Candidate_Application_Status
{
    [Key]
    public int Candidate_Application_Status_id{get;set;}
    [Required]
    public int Candidate_id{get;set;}
    [ForeignKey("Candidate_id")]
    public Candidate_Details candidate_Details{get;set;}
    [Required]
    public int Job_id{get;set;}
    [ForeignKey("Job_id")]
    public Jobs job{get;set;}
    [Required]
    public int Application_Status_id{get;set;}
    [ForeignKey("Application_Status_id")]
    public Application_Status application_Status{get;set;}
    [Required]
    public DateTime Applied_Date{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}