using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Models
{
    public class CourseResponse : ResponseWrapper
    {
        public List<CoursesViewModel>? allCourses { get; set; }
        public CoursesViewModel? courseModel { get; set; } = null;
        public Course? course { get; set; } = null;
       
    }
}
