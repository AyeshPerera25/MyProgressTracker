using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models.Entity
{
    public class Subject
    {
        [Key]
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; } = string.Empty;
        [Required]
        public string Semester { get; set; }
    }
}
