using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Handlers
{
    public class CourseHandler
    {
        private readonly InMemoryDBContext _inMemoryDB;

        public CourseHandler(InMemoryDBContext context)
        {
            _inMemoryDB = context;
        }

        public CourseResponse getAllCourses()
        {
            CourseResponse courseResponse = new CourseResponse();
            
            List < Course > courses = populateAllCourses();
            validateAllCourses(courses);
            populateCorseResponse(courseResponse,courses);
            courseResponse.IsRequestSuccess = true;
            courseResponse.Description = "Course Loading Successful!";
            return courseResponse;
        }

        private void validateAllCourses(List<Course> courses)
        {
            if (courses == null)
            {
                throw new Exception("Course Loading Error!");
            }
            if (courses.Count == 0)
            {
                throw new Exception("No Courses Has Found!");
            }


        }

        private void populateCorseResponse(CourseResponse courseResponse, List<Course> courses)
        {
            CoursesViewModel coursesViewModel;
            List<CoursesViewModel> courseList = new List<CoursesViewModel>();
            if(courses.Count >= 0)
            {
                foreach (Course course in courses)
                {
                    coursesViewModel = new CoursesViewModel();
                    populateCourseViewModel(coursesViewModel, course);
                    populateSemesterCount(coursesViewModel);
                    courseList.Add(coursesViewModel);
                }
            }
            courseResponse.allCourses = courseList;
        }

        private void populateSemesterCount(CoursesViewModel coursesViewModel)
        {
            try
            {
                List<Semester> semesters = _inMemoryDB.Semesters.Where<Semester>(semester => semester.CourseId == coursesViewModel.CourseId).ToList();
                if (semesters != null)
                {
                    coursesViewModel.NoOfSemesters = semesters.Count;
                }
                else
                {
                    coursesViewModel.NoOfSemesters = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Semester Loading Failed! Due to: "+ ex.Message);
            }
        }

        private void populateCourseViewModel(CoursesViewModel coursesViewModel, Course course)
        {
            coursesViewModel.CourseId = course.CourseId;
            coursesViewModel.CourseName = course.CourseName;
            coursesViewModel.CourseStartDate = course.CourseStartDate;
            coursesViewModel.CourseEndDate = course.CourseEndDate;
            coursesViewModel.CourseDescription = course.CourseDescription;
        }

        private List<Course> populateAllCourses()
        {
            return _inMemoryDB.Courses.ToList();
        }
    }
}
