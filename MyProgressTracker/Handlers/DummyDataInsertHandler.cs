using Microsoft.EntityFrameworkCore;
using MyProgressTracker.DataResources;
using MyProgressTrackerDependanciesLib.Models.Entities;

namespace MyProgressTracker.Handlers
{
    public class DummyDataInsertHandler
    {
        private readonly InMemoryDBContext _inMemoryDB;

        public DummyDataInsertHandler(InMemoryDBContext context)
        {
            _inMemoryDB = context;
        }

        public void insertDummyData()
        {
            // Course 1: Computer Science
           /* Course csCourse = new Course(
                courseId: 1,
                courseName: "Computer Science",
                universityName: "ABC University",
                courseStartDate: new DateTime(2024, 9, 1, 9, 0, 0),
                courseEndDate: new DateTime(2025, 6, 1, 18, 0, 0),
                noOfSemesters: 2,
                courseDescription: "A comprehensive course on computer science."
            );
            _inMemoryDB.Courses.Add( csCourse );
            

            Subject csSubject1 = new Subject(
                subjectId: 1,
                subjectName: "Algorithms",
                courseId: 1,
                semesterNo: 1,
                semesterStartDate: new DateTime(2024, 9, 1, 9, 0, 0),
                semesterEndDate: new DateTime(2024, 12, 15, 18, 0, 0),
                subjectDescription: "An in-depth look at algorithms and data structures."
            );
            _inMemoryDB.Subjects.Add( csSubject1 );
            

            Subject csSubject2 = new Subject(
                subjectId: 2,
                subjectName: "Data Structures",
                courseId: 1,
                semesterNo: 1,
                semesterStartDate: new DateTime(2024, 9, 1, 9, 0, 0),
                semesterEndDate: new DateTime(2024, 12, 15, 18, 0, 0),
                subjectDescription: "Understanding data structures and their applications."
            );
            _inMemoryDB.Subjects.Add(csSubject2 );
            

            Subject csSubject3 = new Subject(
                subjectId: 3,
                subjectName: "Operating Systems",
                courseId: 1,
                semesterNo: 2,
                semesterStartDate: new DateTime(2025, 1, 10, 9, 0, 0),
                semesterEndDate: new DateTime(2025, 6, 1, 18, 0, 0),
                subjectDescription: "Introduction to operating systems and their functionalities."
            );
            _inMemoryDB.Subjects.Add(csSubject3);

            Subject csSubject4 = new Subject(
                subjectId: 4,
                subjectName: "Computer Networks",
                courseId: 1,
                semesterNo: 2,
                semesterStartDate: new DateTime(2025, 1, 10, 9, 0, 0),
                semesterEndDate: new DateTime(2025, 6, 1, 18, 0, 0),
                subjectDescription: "Study of computer networks and communication protocols."
            );
            _inMemoryDB.Subjects.Add(csSubject4 );
            // Course 2: Electrical Engineering
            Course eeCourse = new Course(
                courseId: 2,
                courseName: "Electrical Engineering",
                universityName: "XYZ University",
                courseStartDate: new DateTime(2024, 9, 1, 8, 0, 0),
                courseEndDate: new DateTime(2025, 6, 1, 17, 0, 0),
                noOfSemesters: 2,
                courseDescription: "An in-depth course on electrical engineering principles."
            );
            _inMemoryDB.Courses.Add(eeCourse );

            Subject eeSubject1 = new Subject(
                subjectId: 5,
                subjectName: "Circuit Analysis",
                courseId: 2,
                semesterNo: 1,
                semesterStartDate: new DateTime(2024, 9, 1, 8, 0, 0),
                semesterEndDate: new DateTime(2024, 12, 15, 17, 0, 0),
                subjectDescription: "Fundamentals of electrical circuit analysis."
            );
            _inMemoryDB.Subjects.Add(eeSubject1 );

            Subject eeSubject2 = new Subject(
                subjectId: 6,
                subjectName: "Electromagnetics",
                courseId: 2,
                semesterNo: 1,
                semesterStartDate: new DateTime(2024, 9, 1, 8, 0, 0),
                semesterEndDate: new DateTime(2024, 12, 15, 17, 0, 0),
                subjectDescription: "Study of electromagnetics and their applications."
            );
            _inMemoryDB.Subjects.Add(eeSubject2 );

            Subject eeSubject3 = new Subject(
                subjectId: 7,
                subjectName: "Control Systems",
                courseId: 2,
                semesterNo: 2,
                semesterStartDate: new DateTime(2025, 1, 10, 8, 0, 0),
                semesterEndDate: new DateTime(2025, 6, 1, 17, 0, 0),
                subjectDescription: "Introduction to control systems and their design."
            );
            _inMemoryDB.Subjects.Add(eeSubject3 );

            Subject eeSubject4 = new Subject(
                subjectId: 8,
                subjectName: "Power Systems",
                courseId: 2,
                semesterNo: 2,
                semesterStartDate: new DateTime(2025, 1, 10, 8, 0, 0),
                semesterEndDate: new DateTime(2025, 6, 1, 17, 0, 0),
                subjectDescription: "Study of power systems and their operation."
            );
            _inMemoryDB.Subjects.Add(eeSubject4);

            // Course 3: Mechanical Engineering
            Course meCourse = new Course(
                courseId: 3,
                courseName: "Mechanical Engineering",
                universityName: "LMN University",
                courseStartDate: new DateTime(2024, 9, 1, 10, 0, 0),
                courseEndDate: new DateTime(2025, 6, 1, 19, 0, 0),
                noOfSemesters: 2,
                courseDescription: "Comprehensive course on mechanical engineering."
            );
            _inMemoryDB.Courses.Add(meCourse);

            Subject meSubject1 = new Subject(
                subjectId: 9,
                subjectName: "Thermodynamics",
                courseId: 3,
                semesterNo: 1,
                semesterStartDate: new DateTime(2024, 9, 1, 10, 0, 0),
                semesterEndDate: new DateTime(2024, 12, 15, 19, 0, 0),
                subjectDescription: "Introduction to thermodynamics and its applications."
            );
            _inMemoryDB.Subjects.Add(meSubject1 );

            Subject meSubject2 = new Subject(
                subjectId: 10,
                subjectName: "Fluid Mechanics",
                courseId: 3,
                semesterNo: 1,
                semesterStartDate: new DateTime(2024, 9, 1, 10, 0, 0),
                semesterEndDate: new DateTime(2024, 12, 15, 19, 0, 0),
                subjectDescription: "Study of fluid mechanics and its principles."
            );
            _inMemoryDB.Subjects.Add(meSubject2);

            Subject meSubject3 = new Subject(
                subjectId: 11,
                subjectName: "Dynamics",
                courseId: 3,
                semesterNo: 2,
                semesterStartDate: new DateTime(2025, 1, 10, 10, 0, 0),
                semesterEndDate: new DateTime(2025, 6, 1, 19, 0, 0),
                subjectDescription: "Understanding dynamics and its applications."
            );
            _inMemoryDB.Subjects.Add(meSubject3 );

            Subject meSubject4 = new Subject(
                subjectId: 12,
                subjectName: "Material Science",
                courseId: 3,
                semesterNo: 2,
                semesterStartDate: new DateTime(2025, 1, 10, 10, 0, 0),
                semesterEndDate: new DateTime(2025, 6, 1, 19, 0, 0),
                subjectDescription: "Study of material science and engineering materials."
            );
            _inMemoryDB.Subjects.Add(meSubject4 );

            _inMemoryDB.SaveChanges();*/

        }

        private static Random random = new Random();

        // Method to generate random DateTime within a range
        private static DateTime GetRandomDateTime(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan randomSpan = new TimeSpan((long)(random.NextDouble() * timeSpan.Ticks));
            return startDate + randomSpan;
        }

        public static List<StudySession> GenerateStudySessionsForSubject(Subject subject, int sessionCount)
        {
            List<StudySession> studySessions = new List<StudySession>();

            /*for (int i = 0; i < sessionCount; i++)
            {
                DateTime sessionStartTime = GetRandomDateTime(subject.SemesterStartDate, subject.SemesterEndDate);
                DateTime sessionEndTime = sessionStartTime.AddHours(2); // Assuming each session is 2 hours long

                if (sessionEndTime > subject.SemesterEndDate)
                {
                    sessionEndTime = subject.SemesterEndDate;
                }

                StudySession session = new StudySession
                {
                    SessionId = subject.SubjectId * 100 + i + 1, // Ensure unique session IDs
                    SessionName = $"Session {i + 1} for {subject.SubjectName}",
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.SubjectName,
                    SessionStartTime = sessionStartTime,
                    SessionEndTime = sessionEndTime,
                    UserId = 123, // Example user ID
                    Description = $"Study session {i + 1} for {subject.SubjectName}"
                };

                studySessions.Add(session);
            }*/

            return studySessions;
        }
    }
}
