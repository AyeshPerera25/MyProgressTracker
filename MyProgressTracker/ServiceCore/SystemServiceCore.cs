using MyProgressTracker.Handlers;
using MyProgressTracker.Models;
using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.ServiceCore
{
	public class SystemServiceCore 
	{
		private readonly SignInHandler _signInHandler;
        private readonly LoginHandler _loginHandler;
        private readonly CourseHandler _courseHandler;
        private readonly SubjectHandler _subjectHandler;
        private readonly StudySessionHandler _studySessionHandler;
        private readonly DummyDataInsertHandler _dummyDataInsertHandler;
        private readonly ReportHandler _reportHandler;

        public SystemServiceCore(SignInHandler signInHandler, LoginHandler loginHandler, CourseHandler courseHandler, SubjectHandler subjectHandler, StudySessionHandler studySessionHandler, DummyDataInsertHandler dummyDataInsertHandler, ReportHandler reportHandler)
        {
            _signInHandler = signInHandler;
            _loginHandler = loginHandler;
            _courseHandler = courseHandler;
            _subjectHandler = subjectHandler;
            _studySessionHandler = studySessionHandler;
            _dummyDataInsertHandler = dummyDataInsertHandler;
            _reportHandler = reportHandler;
        }

        public void populateDBwithDummy()
        {
            CourseResponse courseResponse = GetAllCourses();
            if (courseResponse != null && courseResponse.allCourses.Count == 0)
            {
                _dummyDataInsertHandler.insertDummyData();
            }


        }
        public List<StudySubjectProgressReportModel> GetProgressReport()
        {
           return _reportHandler.getProgressReport();
        }

		public SignInResponse proccessSignIn(SiginInViewModel signInViewModel)
        {
			SignInResponse signInResponse = null;
			try
            {
				signInResponse = _signInHandler.NewUserRegistration(signInViewModel);
            }
            catch (Exception ex)
            {
				signInResponse = new SignInResponse();
				signInResponse.IsRequestSuccess = false;
				signInResponse.Description = ex.Message;
			}
			return signInResponse;

		}

        public LoginResponse proccessLogin(LoginViewModel loginViewModel)
        {
            LoginResponse loginResponse = null;
            try
            {
                loginResponse = _loginHandler.UserLogin(loginViewModel);
            }
            catch (Exception ex)
            {
                loginResponse = new LoginResponse();
                loginResponse.IsRequestSuccess = false;
                loginResponse.Description = ex.Message;
            }
            return loginResponse;

        }

        public CourseResponse GetAllCourses()
		{
            CourseResponse courseResponse = null;
            try
            {
                courseResponse = _courseHandler.getAllCourses();
            }
            catch (Exception ex)
            {
                courseResponse = new CourseResponse();
                courseResponse.allCourses = new List<CoursesViewModel>();                                                                                     
                courseResponse.IsRequestSuccess = false;
                courseResponse.Description = ex.Message;
            }
            return courseResponse;
        }

        internal CourseResponse AddNewCourse(CoursesViewModel model)
        {
            CourseResponse courseResponse = null;
            try
            {
                courseResponse = _courseHandler.addCourse(model);
            }
            catch (Exception ex)
            {
                courseResponse = new CourseResponse();
                courseResponse.allCourses = new List<CoursesViewModel>();
                courseResponse.IsRequestSuccess = false;
                courseResponse.Description = ex.Message;
            }
            return courseResponse;
        }

        internal SubjectResponse GetAllSubjects()
        {
            SubjectResponse response = null;
            try
            {
                response = _subjectHandler.getAllSubjects();
            }
            catch (Exception ex)
            {
                response = new SubjectResponse();
                response.allSubjects = new List<SubjectViewModel>();
                response.IsRequestSuccess = false;
                response.Description = ex.Message;
            }
            return response;
        }

        internal SubjectResponse? AddNewSubject(AddSubjectViewModel model)
        {
            SubjectResponse? subjectResponse = null; 
            try
            {
                subjectResponse = _subjectHandler.addSubject(model);
            }
            catch (Exception ex)
            {
                subjectResponse = new SubjectResponse();
                subjectResponse.allSubjects = new List<SubjectViewModel>();
                subjectResponse.IsRequestSuccess = false;
                subjectResponse.Description = ex.Message;
            }
            return subjectResponse;
        }

        internal StudySessionResponse GetAllStudySessions()
        {
            StudySessionResponse studySessionResponse = null;
            try
            {
                studySessionResponse = _studySessionHandler.getAllSessions();
            }
            catch (Exception ex)
            {
                studySessionResponse = new StudySessionResponse();
                studySessionResponse.allSessions = new List<StudySessionViewModel>();
                studySessionResponse.IsRequestSuccess = false;
                studySessionResponse.Description = ex.Message;
            }
            return studySessionResponse;
        }

        internal StudySessionResponse AddNewSession(AddSessionViewModel model)
        {
            StudySessionResponse? studySessionResponse = null;
            try
            {
                studySessionResponse = _studySessionHandler.addSession(model);
            }
            catch (Exception ex)
            {
                studySessionResponse = new StudySessionResponse();
                studySessionResponse.allSessions = new List<StudySessionViewModel>();
                studySessionResponse.IsRequestSuccess = false;
                studySessionResponse.Description = ex.Message;
            }
            return studySessionResponse;
        }
    }
}
