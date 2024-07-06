using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProgressTracker.Models.Entity
{
    public class StudySession
    {
        [Key]
        [Required]
        public int SessionId { get; set; }
        [Required]
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime SessionStartTime { get; set; }
        [Required]
        public DateTime SessionEndTime { get; set; }
        public string? Description { get; set; } 
        
        

    }
}
