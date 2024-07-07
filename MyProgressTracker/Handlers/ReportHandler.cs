using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Handlers
{
    public class ReportHandler
    {
        private readonly InMemoryDBContext _inMemoryDB;

        public ReportHandler(InMemoryDBContext context)
        {
            _inMemoryDB = context;
        }

        public List<StudySubjectProgressReportModel> getProgressReport()
        {
            StudySubjectProgressReportModel model;
            List<StudySubjectProgressReportModel> modelList = new List<StudySubjectProgressReportModel>();  
            List<StudySession> sessions = _inMemoryDB.StudySessions.ToList();
            List<StudySession> filteredSessions;
            if (sessions.Count > 0) {
                _inMemoryDB.Subjects.ToList().ForEach(subject =>
                {
                    model = new StudySubjectProgressReportModel();
                    filteredSessions = sessions.FindAll(session => session.SubjectId == subject.SubjectId).ToList();
                    TimeSpan totalStudyTime = TimeSpan.Zero;
                    filteredSessions.ForEach(session =>
                    {
                        totalStudyTime = totalStudyTime + (session.SessionEndTime - session.SessionStartTime);
                    });

                    model.NoOfSessions = filteredSessions.Count;
                    model.CourseName = _inMemoryDB.Courses.SingleOrDefault<Course>(course => course.CourseId == subject.CourseID).CourseName;
                    model.SubjectName = subject.SubjectName;
                    TimeSpan totalTimeForSubject = subject.SemesterEndDate - subject.SemesterStartDate;
                    model.TotalCourseDuration = totalTimeForSubject;
                    model.TotalStudyDuration = totalStudyTime;
                    model.Score = (totalStudyTime.TotalHours / totalTimeForSubject.TotalHours) * 100;
                    modelList.Add(model);
                });
            }
            return modelList;
        }
    }
}
