using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Candidate_Skills
{
    [Key]
    public int Candidate_Skill_id{get;set;}
    [Required]
    public int Candidate_id{get;set;}
    [ForeignKey("Candidate_id")]
    public Candidate_Details candidate_Details{get;set;}
    [Required]
    public int Skill_id{get;set;}
    [ForeignKey("Skill_id")]
    public Skill skill{get;set;}
    [Required]
    public int Total_Skill_Work_experience{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}