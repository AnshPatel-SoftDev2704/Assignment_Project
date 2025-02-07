using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Interviewer
{
    [Key]
    public int Interviewer_id{get;set;}
    [Required]
    public int Interview_id{get;set;}
    [ForeignKey("Interview_id")]
    public Interview interview{get;set;}
    [Required]
    public int User_id{get;set;}
    [ForeignKey("User_id")]
    public User user{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}