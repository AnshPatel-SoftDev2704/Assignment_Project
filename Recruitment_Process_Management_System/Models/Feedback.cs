using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Feedback
{
    [Key]
    public int Feedback_id{get;set;}
    [Required]
    public int Interview_id{get;set;}
    [ForeignKey("Interview_id")]
    public Interview interview{get;set;}
    [Required]
    public int User_id{get;set;}
    [ForeignKey("User_id")]
    public User user{get;set;}
    [Required]
    public string Technology{get;set;}
    [Required]
    public int rating{get;set;}
    [Required]
    public string comments{get;set;}
    [Required]
    public DateTime submitted_date{get;set;}
    [Required]
    public DateTime Created_at{get;set;}
    [Required]
    public DateTime Updated_at{get;set;}
}