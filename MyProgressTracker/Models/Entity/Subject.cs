using NuGet.Protocol.Plugins;
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
        [Required]
        public int CourseID { get; set;}
        public string? SubjectDescription { get; set; } 
        [Required]
        public int SemesterNo { get; set; }
        [Required]
        public DateTime SemesterStartDate { get; set; }
        [Required]
        public DateTime SemesterEndDate { get; set; }

        // Default constructor
        public Subject() { }

        // Parameterized constructor
        public Subject(int subjectId, string subjectName, int courseId, int semesterNo, DateTime semesterStartDate, DateTime semesterEndDate, string? subjectDescription = null)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
            CourseID = courseId;
            SemesterNo = semesterNo;
            SemesterStartDate = semesterStartDate;
            SemesterEndDate = semesterEndDate;
            SubjectDescription = subjectDescription;
        }
    }
}
