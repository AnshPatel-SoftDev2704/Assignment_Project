using System.ComponentModel.DataAnnotations;

namespace Recruitment_Process_Management_System.Models;

public class Interview_Type
{
    [Key]
    public int Interview_Type_id{get;set;}
    [Required]
    public string Interview_Type_name{get;set;}
    [Required]
    public string Interview_Type_description{get;set;}
    public DateTime Created_at{get;set;}
    public DateTime Updated_at{get;set;}
}