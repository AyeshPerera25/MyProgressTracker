using MyProgressTracker.Handlers;
using MyProgressTracker.Models;
using MyProgressTrackerDependanciesLib.Models.Entities;

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

        public void populateDBwithDummy(string? sessionKey, long userID)
        {
            CourseResponse courseResponse = GetAllCourses(sessionKey, userID);
            if (courseResponse != null && courseResponse.allCourses.Count == 0)
            {
                _dummyDataInsertHandler.insertDummyData();
            }


        }
        public List<StudySubjectProgressReportModel> GetProgressReport(string? sessionKey, long userID)
        {
			List<StudySubjectProgressReportModel> reportModelList = null;
			try
			{
				reportModelList = _reportHandler.getProgressReport(sessionKey, userID);
			}
			catch (Exception ex)
			{
				reportModelList = new List<StudySubjectProgressReportModel>();
				
			}
			return reportModelList;
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

        public CourseResponse GetAllCourses(string? sessionKey , long userID)
		{
            CourseResponse courseResponse = null;
            try
            {
                courseResponse = _courseHandler.getAllCourses(sessionKey, userID);
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

        internal CourseResponse AddNewCourse(CoursesViewModel model, string? sessionKey, long userID)
        {
            CourseResponse courseResponse = null;
            try
            {
                courseResponse = _courseHandler.addCourse(model, sessionKey, userID);
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

        internal SubjectResponse GetAllSubjects(string? sessionKey, long userID)
        {
            SubjectResponse response = null;
            try
            {
                response = _subjectHandler.getAllSubjects(sessionKey,userID);
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

        internal SubjectResponse? AddNewSubject(AddSubjectViewModel model, string? sessionKey, long userID)
        {
            SubjectResponse? subjectResponse = null; 
            try
            {
                subjectResponse = _subjectHandler.addSubject(model, sessionKey, userID);
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

        internal StudySessionResponse GetAllStudySessions(string? sessionKey, long userID)
        {
            StudySessionResponse studySessionResponse = null;
            try
            {
                studySessionResponse = _studySessionHandler.getAllSessions(sessionKey, userID);
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

        internal StudySessionResponse AddNewSession(AddSessionViewModel model, string? sessionKey, long userID)
        {
            StudySessionResponse? studySessionResponse = null;
            try
            {
                studySessionResponse = _studySessionHandler.addSession(model, sessionKey, userID);
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

        internal LogoutResponse processLogout(string? sessionKey, long userID)
        {
            LogoutResponse? response = null;
            try
            {
                response = _loginHandler.UserLogout(sessionKey, userID);
            }
            catch (Exception ex)
            {
                response = new LogoutResponse();
                response.IsRequestSuccess = false;
                response.Description = ex.Message;
            }
            return response;

        }
    }
}
