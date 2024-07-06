using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models
{
    public class CoursesViewModel
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string UniversityName { get; set; }
        public string? CourseDescription { get; set; }
        [Required]
        public DateTime CourseStartDate { get; set; }
        [Required]
        public DateTime CourseEndDate { get; set; }
        public int? NoOfSemesters { get; set; }

    }
}
