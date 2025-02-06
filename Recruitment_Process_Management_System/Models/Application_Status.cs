using System.ComponentModel.DataAnnotations;

namespace Recruitment_Process_Management_System.Models;

public class Application_Status
{
    [Key]
    public int Application_Status_id{get;set;}
    [Required]
    public string Application_Status_Name{get;set;}
    [Required]
    public string Application_Status_Description{get;set;}
    public DateTime Created_at{get;set;}
    public DateTime Updated_at{get;set;}
}