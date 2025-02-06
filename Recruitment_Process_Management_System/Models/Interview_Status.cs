using System.ComponentModel.DataAnnotations;

namespace Recruitment_Process_Management_System.Models;

public class Interview_Status
{
    [Key]
    public int Interview_Status_id{get;set;}
    [Required]
    public string Interview_Status_name{get;set;}
    [Required]
    public string Interview_Status_description{get;set;}
    public DateTime Created_at{get;set;}
    public DateTime Updated_at{get;set;}
}