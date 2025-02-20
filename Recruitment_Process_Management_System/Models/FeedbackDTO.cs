namespace Recruitment_Process_Management_System.Models;

public class FeedbackDTO
{
    public int Interview_id{get;set;}
    public int User_id{get;set;}
    public int Technology{get;set;}
    public int rating{get;set;}
    public string comments{get;set;}
    public DateTime submitted_date{get;set;}
}