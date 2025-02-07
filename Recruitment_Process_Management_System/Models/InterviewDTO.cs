namespace Recruitment_Process_Management_System.Models;

public class InterviewDTO
{
    public int Application_id{get;set;}
    public int Number_of_round{get;set;}
    public int Interview_Type_id{get;set;}
    public DateTime Scheduled_at{get;set;}
    public int Interview_Status_id{get;set;}
    public string Special_Notes{get;set;}
    public string Interview_Meeting_Link{get;set;}
}