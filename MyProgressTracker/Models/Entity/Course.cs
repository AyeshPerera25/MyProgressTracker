using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models.Entity
{
    public class Course
    {
        [Key]
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string UniversityName { get; set; }
        public string? CourseDescription { get; set; }
        [Required]
        public DateTime CourseStartDate {  get; set; }
        [Required]
        public DateTime CourseEndDate { get; set; }
        [Required]
        public int NoOfSemesters { get; set; }

        // Default constructor
        public Course() { }

        // Parameterized constructor
        public Course(int courseId, string courseName, string universityName, DateTime courseStartDate, DateTime courseEndDate, int noOfSemesters, string? courseDescription = null)
        {
            CourseId = courseId;
            CourseName = courseName;
            UniversityName = universityName;
            CourseStartDate = courseStartDate;
            CourseEndDate = courseEndDate;
            NoOfSemesters = noOfSemesters;
            CourseDescription = courseDescription;
        }
    }
}
