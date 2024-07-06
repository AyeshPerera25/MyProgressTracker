using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models.Entity
{
    public class Semester
    {
        [Key]
        [Required]
        public int SemesterId { get; set; }
        [Required]
        public string SemesterName { get; set; }
        [Required]
        public int CourseId { get; set; }
        public string? SemesterDescription { get; set; }
        [Required]
        public DateTime SemesterStartDate { get; set; }
        [Required]
        public DateTime SemesterEndDate { get; set; }
    }
}
