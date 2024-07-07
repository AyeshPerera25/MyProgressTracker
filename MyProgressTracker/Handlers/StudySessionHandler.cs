using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.Models.Entity;
using System.Linq;

namespace MyProgressTracker.Handlers
{
    public class StudySessionHandler
    {
        private readonly InMemoryDBContext _inMemoryDB;

        public StudySessionHandler(InMemoryDBContext context)
        {
            _inMemoryDB = context;
        }

        internal StudySessionResponse? getAllSessions()
        {
            StudySessionResponse studySessionResponse = new StudySessionResponse();

            List<StudySession> sessions = populateAllSessions();
            validateAllSessions(sessions);
            populateStudySessionResponse(studySessionResponse, sessions);
            studySessionResponse.IsRequestSuccess = true;
            studySessionResponse.Description = "SessionLoading Successful!";
            return studySessionResponse;
        }

        private void populateStudySessionResponse(StudySessionResponse studySessionResponse, List<StudySession> sessions)
        {
            StudySessionViewModel studySessionViewModel;
            List<StudySessionViewModel> sessionList = new List<StudySessionViewModel>();
            Subject? subject;
            Course? course;
            if (sessions.Count >= 0)
            {
                foreach (StudySession session in sessions)
                {
                    studySessionViewModel = new StudySessionViewModel();
                    subject = loadSubjectForSession(session.SubjectId);
                    course = loadCourseForSession(subject.CourseID);
                    populateStudySessionViewModel(studySessionViewModel,session,course,subject);

                    sessionList.Add(studySessionViewModel);
                }
            }
            studySessionResponse.allSessions = sessionList;
        }

        private void populateStudySessionViewModel(StudySessionViewModel studySessionViewModel, StudySession session, Course? course, Subject subject)
        {
            studySessionViewModel.SessionId = session.SessionId;
            studySessionViewModel.SessionName = session.SessionName;
            studySessionViewModel.SessionDescription = session.Description;
            studySessionViewModel.SessionStartTime = session.SessionStartTime;
            studySessionViewModel.SessionEndTime = session.SessionEndTime;
            studySessionViewModel.SemestersNo = subject.SemesterNo;
            studySessionViewModel.SubjectID = subject.SubjectId;
            studySessionViewModel.SubjectName = subject.SubjectName;
            studySessionViewModel.CourseID = course.CourseId;
            studySessionViewModel.CourseName = course.CourseName;

        }

        private Course? loadCourseForSession(int courseID)
        {
            if (courseID == 0)
            {
                throw new Exception("Course Loading Error For Session!");
            }
            Course? course = _inMemoryDB.Courses.SingleOrDefault<Course>(_course => _course.CourseId == courseID);
            if (course == null)
            {
                throw new Exception("Course Loading Error For Session!");
            }
            return course;
        }

        private Subject? loadSubjectForSession(int subjectId)
        {

            if (subjectId == 0)
            {
                throw new Exception("Subject Loading Error For Session!");
            }
            Subject? subject = _inMemoryDB.Subjects.SingleOrDefault<Subject>(sub => sub.SubjectId == subjectId);
            if (subject == null)
            {
                throw new Exception("Subject Loading Error For Session!");
            }
            return subject;
        }

        private void validateAllSessions(List<StudySession> sessions)
        {
            if (sessions == null)
            {
                throw new Exception("Sessions Loading Error!");
            }
            if (sessions.Count == 0)
            {
                throw new Exception("No Sessions Has Found!");
            }
        }

        private List<StudySession> populateAllSessions()
        {
            return _inMemoryDB.StudySessions.ToList();
        }

        internal StudySessionResponse? addSession(AddSessionViewModel model)
        {
            StudySessionResponse response = new StudySessionResponse();
            StudySession session = new StudySession();
            validateNewSessionReqModel(model);
            Subject subject = loadSubjectForSession(model.Session.SubjectID);
            populateNewSession(session, model,subject);
            persistNewSession(session);
            response.IsRequestSuccess = true;
            response.Description = "Add Subject Successful!";
            response.session = session;
            response.sessionModel = model.Session;
            return response;
        }

        private void persistNewSession(StudySession session)
        {
            _inMemoryDB.StudySessions.Add(session);
            _inMemoryDB.SaveChanges();
        }

        private void populateNewSession(StudySession session, AddSessionViewModel model, Subject subject)
        {
            session.SessionName = model.Session.SessionName;
            session.SubjectId = model.Session.SubjectID;
            session.SubjectName = subject.SubjectName ;
            session.SessionStartTime =  model.Session.SessionStartTime;
            session.SessionEndTime = model.Session.SessionEndTime;  
            session.Description = model.Session.SessionDescription;
        }

        private void validateNewSessionReqModel(AddSessionViewModel model)
        {
            if (model.Session == null)
            {
                throw new Exception("New Session Request Model is Null! ");
            }
            if (model.Session.SessionName == null || model.Session.SessionName == "")
            {
                throw new Exception("New Session Name has not defined! ");
            }
            if (model.Session.SubjectID == 0)
            {
                throw new Exception("New Session Subject not found!");
            }
        }
    }
}
