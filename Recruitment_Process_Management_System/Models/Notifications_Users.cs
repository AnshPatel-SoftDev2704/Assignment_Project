using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;
public class Notifications_Users
{
    [Key]
    public int Notifications_Users_id{get;set;}
    [Required]
    public int User_id{get;set;}
    [ForeignKey("User_id")]
    public User user{get;set;}
    [Required]
    public string Message{get;set;}
    [Required]
    public bool Status{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}