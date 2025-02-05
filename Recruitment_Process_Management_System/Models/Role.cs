using System.ComponentModel.DataAnnotations;
namespace Recruitment_Process_Management_System.Models;

public class Role
{
    [Key]
    public int Role_id{get; set;}
    [Required]
    public string Role_Name {get;set;}
    [Required]
    public string Role_Description {get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}

}