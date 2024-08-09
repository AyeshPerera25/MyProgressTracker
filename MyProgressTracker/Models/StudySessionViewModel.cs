using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models
{
    public class StudySessionViewModel
    {
        [Required]
        public int SessionId { get; set; }
        [Required]
        public string SessionName { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public int SubjectID { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public string? SessionDescription { get; set; }
        [Required]
        public DateTime SessionStartTime { get; set; }
        [Required]
        public DateTime SessionEndTime { get; set; }
        [Required]
        public int SemestersNo { get; set; }
    }
}
