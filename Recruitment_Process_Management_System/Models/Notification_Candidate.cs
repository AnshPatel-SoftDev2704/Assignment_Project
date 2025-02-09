using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;
public class Notifications_Candidate
{
    [Key]
    public int Notifications_Candidates_id{get;set;}
    [Required]
    public int Candidate_id{get;set;}
    [ForeignKey("Candidate_id")]
    public Candidate_Details candidate{get;set;}
    [Required]
    public string Message{get;set;}
    [Required]
    public bool Status{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}