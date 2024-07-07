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

        // Default constructor
        public Semester() { }

        // Parameterized constructor
        public Semester(int semesterId, string semesterName, int courseId, DateTime semesterStartDate, DateTime semesterEndDate, string? semesterDescription = null)
        {
            SemesterId = semesterId;
            SemesterName = semesterName;
            CourseId = courseId;
            SemesterStartDate = semesterStartDate;
            SemesterEndDate = semesterEndDate;
            SemesterDescription = semesterDescription;
        }
    }
}
