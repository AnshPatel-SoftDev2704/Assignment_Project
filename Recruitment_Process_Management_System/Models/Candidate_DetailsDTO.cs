namespace Recruitment_Process_Management_System.Models;

public class Candidate_DetailsDTO
{
    public string Candidate_name{get;set;}
    public DateTime Candidate_DOB{get;set;}
    public string Candidate_email{get;set;}
    public string Candidate_password{get;set;}
    public int Candidate_Total_Work_Experience{get;set;}
    public int PhoneNo{get;set;}
    public string CV_Path{get;set;}
    public int Role_id{get;set;}
}