using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Required_Job_Skill
{
    [Key]
    public int Required_Job_Skill_id{get;set;}
    [Required]
    public int Job_id{get;set;}
    [ForeignKey("Job_id")]
    public Jobs jobs{get;set;}
    [Required]
    public int Skill_id{get;set;}
    [ForeignKey("Skill_id")]
    public Skill skill{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}