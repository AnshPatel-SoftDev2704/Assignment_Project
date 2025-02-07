using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Process_Management_System.Models;

public class Document_Submitted
{
    [Key]
    public int Document_Submitted_id{get;set;}
    [Required]
    public int Candidate_id{get;set;}
    [ForeignKey("Candidate_id")]
    public Candidate_Details? candidate{get;set;}
    [Required]
    public int Job_id{get;set;}
    [ForeignKey("Job_id")]
    public Jobs? job{get;set;}
    [Required]
    public int Document_Type_id{get;set;}
    [ForeignKey("Document_Type_id")]
    public Document_Type? document_Type{get;set;}
    [Required]
    public bool Status{get;set;}
    [Required]
    public int Approved_by{get;set;}
    [ForeignKey("Approved_by")]
    public User? user{get;set;}
    [Required]
    public string? Document_path{get;set;}
    [Required]
    public DateTime Submitted_date{get;set;}
    public DateTime Approved_date{get;set;}
    public DateTime Created_at{get;set;}
    public DateTime Updated_at{get;set;}
}