using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Handlers
{
    public class SubjectHandler
    {
        private readonly InMemoryDBContext _inMemoryDB;

        public SubjectHandler(InMemoryDBContext context)
        {
            _inMemoryDB = context;
        }

        internal SubjectResponse? getAllSubjects()
        {
            SubjectResponse subjectResponse = new SubjectResponse();

            List<Subject> subjects = populateAllSubjects();
            validateAllSubjects(subjects);
            populateSubjectResponse(subjectResponse, subjects);
            subjectResponse.IsRequestSuccess = true;
            subjectResponse.Description = "Subjects Loading Successful!";
            return subjectResponse;
        }

        private void populateSubjectResponse(SubjectResponse subjectResponse, List<Subject> subjects)
        {
            SubjectViewModel subjectViewModel;
            List<SubjectViewModel> subjectList = new List<SubjectViewModel>();
            Course? course;
            if (subjects.Count >= 0)
            {
                foreach (Subject subject in subjects)
                {
                    subjectViewModel = new SubjectViewModel();
                    course = loadCourseForSubject(subject.CourseID);
                    populateSubjectViewModel(subjectViewModel, subject, course);
                    subjectList.Add(subjectViewModel);
                }
            }
            subjectResponse.allSubjects = subjectList;
        }

        private Course loadCourseForSubject(int courseID)
        {
            Course? course = _inMemoryDB.Courses.SingleOrDefault<Course>(course => course.CourseId == courseID);
            if(course == null)
            {
                throw new Exception("Course Loading Error For Subject!");
            }
            return course;
        }

        private void populateSubjectViewModel(SubjectViewModel subjectViewModel, Subject subject, Course course)
        {
            subjectViewModel.SubjectId = subject.SubjectId;
            subjectViewModel.SubjectName = subject.SubjectName;
            subjectViewModel.CourseID = course.CourseId;
            subjectViewModel.CourseName = course.CourseName;
            subjectViewModel.universityName = course.UniversityName;
            subjectViewModel.SemesterNo = subject.SemesterNo;
            subjectViewModel.SemesterStartDate = subject.SemesterStartDate;
            subjectViewModel.SemesterEndDate = subject.SemesterEndDate;
            subjectViewModel.Description = subject.SubjectDescription;
        }

        private void validateAllSubjects(List<Subject> subjects)
        {
            if (subjects == null)
            {
                throw new Exception("Subject Loading Error!");
            }
            if (subjects.Count == 0)
            {
                throw new Exception("No Subject Has Found!");
            }
        }

        private List<Subject> populateAllSubjects()
        {
            return _inMemoryDB.Subjects.ToList();
        }

        internal SubjectResponse? addSubject(AddSubjectViewModel model)
        {
            SubjectResponse subjectResponse = new SubjectResponse();
            Subject subject = new Subject();
            validateNewSubjectReqModel(model);
            populateNewSubject(subject, model);
            persistNewSubject(subject);
            subjectResponse.IsRequestSuccess = true;
            subjectResponse.Description = "Add Subject Successful!";
            subjectResponse.subject = subject;
            subjectResponse.subjectModel = model.Subject;
            return subjectResponse;
        }

        private void persistNewSubject(Subject subject)
        {
            _inMemoryDB.Subjects.Add(subject);
            _inMemoryDB.SaveChanges();
        }

        private void populateNewSubject(Subject subject, AddSubjectViewModel model)
        {
            subject.SubjectName = model.Subject.SubjectName;
            subject.SemesterNo = model.Subject.SemesterNo;
            subject.SemesterStartDate = model.Subject.SemesterStartDate;
            subject.SemesterEndDate = model.Subject.SemesterEndDate;
            subject.SubjectDescription = model.Subject.Description;
            subject.CourseID = model.Subject.CourseID;
        }

        private void validateNewSubjectReqModel(AddSubjectViewModel model)
        {
            if (model.Subject == null)
            {
                throw new Exception("New Subject Request Model is Null! ");
            }
            if (model.Subject.SubjectName == null || model.Subject.SubjectName == "")
            {
                throw new Exception("New Subject Name has not defined! ");
            }
            if (model.Subject.CourseID == 0)
            {
                throw new Exception("New Subject CourseId not found!");
            }
        }
    }
}
