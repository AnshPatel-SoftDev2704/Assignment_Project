using System.ComponentModel.DataAnnotations;

namespace Recruitment_Process_Management_System.mdoels;

public class Document_Type
{
    [Key]
    public int Document_Type_id{get;set;}
    [Required]
    public string Document_name{get;set;}
    [Required]
    public string Document_type{get;set;}
    public DateTime Created_at{get;set;}
    public DateTime Updated_at{get;set;}
}