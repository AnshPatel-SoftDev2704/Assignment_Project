using System.ComponentModel.DataAnnotations;

namespace Recruitment_Process_Management_System.Models;

public class Job_Status
{
    [Key]
    public int Job_Status_id {get;set;}
    [Required]
    public string Job_Status_name {get;set;}
    [Required]
    public string Job_Status_Description {get;set;}
    public DateTime Created_at {get;set;}
    public DateTime Updated_at {get;set;}
}