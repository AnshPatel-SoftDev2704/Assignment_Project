using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Interview
{
    [Key]
    public int Interview_id{get;set;}
    [Required]
    public int Application_id{get;set;}
    [ForeignKey("Application_id")]
    public Candidate_Application_Status candidate_Application_Status{get;set;}
    [Required]
    public int Number_of_round{get;set;}
    [Required]
    public int Interview_Type_id{get;set;}
    [ForeignKey("Interview_Type_id")]
    public Interview_Type interview_Type{get;set;}
    [Required]
    public DateTime Scheduled_at{get;set;}
    [Required]
    public int Interview_Status_id{get;set;}
    [ForeignKey("Interview_Status_id")]
    public Interview_Status interview_Status{get;set;}
    [Required]
    public string Special_Notes{get;set;}
    [Required]
    public string Interview_Meeting_Link{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}