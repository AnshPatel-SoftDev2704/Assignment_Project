using System.ComponentModel.DataAnnotations;

namespace Recruitment_Process_Management_System.Models
{
    public class Skill
    {
        [Key]
        public int Skill_id {get;set;}
        [Required]
        public string Skill_name{get;set;}
        [Required]
        public string Skill_description{get;set;}
        [Required]
        public DateTime Created_at{get;set;}
        [Required]
        public DateTime Updated_at{get;set;}
    }
}