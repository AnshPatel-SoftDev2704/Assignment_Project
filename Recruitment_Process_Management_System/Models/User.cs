using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class User
{
    [Key]
    public int User_id {get; set;}
    public string name{get;set;}
    public string email{get;set;}
    public string contact{get;set;}
    public string password{get;set;}
    public DateTime Created_at { get; set; } 
    public DateTime Updated_at { get; set; }
}