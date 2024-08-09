using MyProgressTrackerDependanciesLib.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models
{
    public class SubjectViewModel
    {
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string universityName { get; set; }
        [Required]
        public int SemesterNo { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime SemesterStartDate { get; set; }
        [Required]
        public DateTime SemesterEndDate { get; set; }
    }
}
