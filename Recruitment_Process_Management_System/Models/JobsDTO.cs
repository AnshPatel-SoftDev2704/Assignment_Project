namespace Recruitment_Process_Management_System.Models;

public class JobsDTO
{
    public string Job_title{get;set;}
    public string Job_description{get;set;}
    public int Job_Status_id{get;set;}
    public string Job_Closed_reason{get;set;}
    public int Job_Selected_Candidate_id{get;set;}
    public int Created_by{get;set;}
    
}