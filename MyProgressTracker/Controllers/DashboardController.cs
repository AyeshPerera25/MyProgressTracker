using Microsoft.AspNetCore.Mvc;
using MyProgressTracker.Models;
using MyProgressTracker.ServiceCore;

namespace MyProgressTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SystemServiceCore _systemServiceCore;

        public DashboardController (SystemServiceCore systemService)
        {
            _systemServiceCore = systemService;
        }

        //======================================================================================================================>>>  GET 

        //------------------------------------------------------------------------------------------ DashboardView 
        public IActionResult DashboardView()
        {
			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}
            List<StudySubjectProgressReportModel> studySubjectProgressReportModels = _systemServiceCore.GetProgressReport(sessionKey, long.Parse(userID));
            DashboardViewModel model = new DashboardViewModel();
            model.ReportModelList = studySubjectProgressReportModels;

            return View(model);
        }
        //------------------------------------------------------------------------------------------ CoursesView 
        public IActionResult CoursesView()
        {
            string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
            string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
            if(userID == null || userID == string.Empty)
            {
                userID = string.Concat(0L);
            }

            CourseResponse response = _systemServiceCore.GetAllCourses(sessionKey,long.Parse(userID));
            if (response != null)
            {
                
                TempData["message"] = response?.Description ?? "Unable to load courses";
                

                if (response.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error
                }
            }
            else
            {

            }
            return View(response.allCourses);
        }
        //------------------------------------------------------------------------------------------ SubjectView 
        public IActionResult SubjectsView()
        {
			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}

			SubjectResponse response = _systemServiceCore.GetAllSubjects(sessionKey, long.Parse(userID));
            if (response != null)
            {
                TempData["message"] = response?.Description ?? "Unable to load subjects";

                if (response.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error
                }
            }
            return View(response.allSubjects);
        }
        //------------------------------------------------------------------------------------------ StudySessionsView 
        public IActionResult StudySessionsView()
        {
			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}
			StudySessionResponse response = _systemServiceCore.GetAllStudySessions(sessionKey, long.Parse(userID));
            if (response != null)
            {
                TempData["message"] = response?.Description ?? "Unable to load sessions";

                if (response.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error
                }
            }
            return View(response.allSessions);
        }
        //------------------------------------------------------------------------------------------ AddCourseView 
        public IActionResult AddCourseView()
        {
            return View();
        }
        //------------------------------------------------------------------------------------------ AddSubjectView 
        public IActionResult AddSubjectView()
        {
			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}

			AddSubjectViewModel addSubjectModel = new AddSubjectViewModel();
            addSubjectModel.Subject = new SubjectViewModel();
            CourseResponse courseResponse = _systemServiceCore.GetAllCourses(sessionKey,long.Parse(userID));
            TempData["message"] = courseResponse?.Description ?? "Unable to add course!";
            if (courseResponse != null)
            {

                if (courseResponse.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error
                    return RedirectToAction("CoursesView");
                }
                if (courseResponse.allCourses == null || courseResponse.allCourses.Count == 0)
                {
                    TempData["messageTyp"] = "E"; // Error
                    return RedirectToAction("AddCourseView");
                }
                addSubjectModel.Courses = courseResponse.allCourses;
            }
            else
            {
                TempData["messageTyp"] = "E"; // Error
                addSubjectModel.Courses = new List<CoursesViewModel>();
                return RedirectToAction("SubjectsView");
            }
            return View(addSubjectModel);
        }
        //------------------------------------------------------------------------------------------ AddSessionView 
        public IActionResult AddSessionView()
        {
            AddSessionViewModel addSessionViewModel = new AddSessionViewModel();
            addSessionViewModel.Session = new StudySessionViewModel();
			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}
			SubjectResponse subjectResponse = _systemServiceCore.GetAllSubjects(sessionKey, long.Parse(userID));
            TempData["message"] = subjectResponse?.Description ?? "Unable to add subjects!";
            if (subjectResponse != null)
            {

                if (subjectResponse.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error
                    return RedirectToAction("SubjectsView");
                }
                if (subjectResponse.allSubjects == null || subjectResponse.allSubjects.Count == 0)
                {
                    TempData["messageTyp"] = "E"; // Error
                    return RedirectToAction("SubjectsView");
                }
                addSessionViewModel.subjects = subjectResponse.allSubjects;
            }
            else
            {
                TempData["messageTyp"] = "E"; // Error
                addSessionViewModel.subjects = new List<SubjectViewModel>();
                return RedirectToAction("StudySessionsView");
            }

            return View(addSessionViewModel);
        }

        //======================================================================================================================>>>  POST
        public IActionResult AddCourse(CoursesViewModel model)
        {

			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}

			CourseResponse response = _systemServiceCore.AddNewCourse(model, sessionKey,long.Parse(userID));
            if (response != null)
            {
                if (response.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                    return RedirectToAction("CoursesView");
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error
                }
                return RedirectToAction("AddCourseView");
            }
            else 
            {
                TempData["messageTyp"] = "E"; // Error
            }
            TempData["message"] = response?.Description ?? "Unable to add course!";
            return RedirectToAction("AddCourseView");
        }

        public IActionResult AddSubject(AddSubjectViewModel model)
        {
			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}
			SubjectResponse response = _systemServiceCore.AddNewSubject(model, sessionKey, long.Parse(userID));
            if (response != null)
            {
                if (response.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                    return RedirectToAction("SubjectsView");
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error   
                }
                return RedirectToAction("AddSubjectView");
            }
            else
            {
                TempData["messageTyp"] = "E"; // Error
            }
            TempData["message"] = response?.Description ?? "Unable to add course!";
            return RedirectToAction("AddSubjectView");
        }

        public IActionResult AddSession(AddSessionViewModel model)
        {
			string? sessionKey = HttpContext.Session.GetString(SystemConstant.SessionKey);
			string? userID = HttpContext.Session.GetString(SystemConstant.UserID);
			if (userID == null || userID == string.Empty)
			{
				userID = string.Concat(0L);
			}
			StudySessionResponse response = _systemServiceCore.AddNewSession(model, sessionKey, long.Parse(userID));
            if (response != null)
            {
                if (response.IsRequestSuccess)
                {
                    TempData["messageTyp"] = "S"; // Success
                    return RedirectToAction("StudySessionsView");
                }
                else
                {
                    TempData["messageTyp"] = "E"; // Error   
                }
                return RedirectToAction("AddSessionView");
            }
            else
            {
                TempData["messageTyp"] = "E"; // Error
            }
            TempData["message"] = response?.Description ?? "Unable to add course!";
            return RedirectToAction("AddSessionView");
        }


    }
}
