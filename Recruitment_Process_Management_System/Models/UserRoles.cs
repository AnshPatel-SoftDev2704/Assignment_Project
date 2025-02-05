using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class UserRoles
{
    [Key]
    public int UserRoleId{get;set;}
    [Required]
    public int User_id{get;set;}
    [ForeignKey("User_id")]
    public User user{get;set;}
    [Required]
    public int Role_id{get;set;}
    [ForeignKey("Role_id")]
    public Role role{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}