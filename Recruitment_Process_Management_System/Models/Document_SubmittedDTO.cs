namespace Recruitment_Process_Management_System.Models;

public class Document_SubmittedDTO
{
    public int Candidate_id{get;set;}
    public int Job_id{get;set;}
    public int Document_Type_id{get;set;}
    public bool Status{get;set;}
    public int Approved_by{get;set;}
    public string? Document_path{get;set;}
    public DateTime Submitted_date{get;set;}
    public DateTime Approved_date{get;set;}
}