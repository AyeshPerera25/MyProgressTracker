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
            _systemServiceCore.populateDBwithDummy();
            List<StudySubjectProgressReportModel> studySubjectProgressReportModels = _systemServiceCore.GetProgressReport();
            DashboardViewModel model = new DashboardViewModel();
            model.ReportModelList = studySubjectProgressReportModels;

            return View(model);
        }
        //------------------------------------------------------------------------------------------ CoursesView 
        public IActionResult CoursesView()
        {
            CourseResponse response = _systemServiceCore.GetAllCourses();
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
            return View(response.allCourses);
        }
        //------------------------------------------------------------------------------------------ SubjectView 
        public IActionResult SubjectsView()
        {
            SubjectResponse response = _systemServiceCore.GetAllSubjects();
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
            StudySessionResponse response = _systemServiceCore.GetAllStudySessions();
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
            AddSubjectViewModel addSubjectModel = new AddSubjectViewModel();
            addSubjectModel.Subject = new SubjectViewModel();
            CourseResponse courseResponse = _systemServiceCore.GetAllCourses();
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
            SubjectResponse subjectResponse = _systemServiceCore.GetAllSubjects();
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
            CourseResponse response = _systemServiceCore.AddNewCourse(model);
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
            SubjectResponse response = _systemServiceCore.AddNewSubject(model);
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
            StudySessionResponse response = _systemServiceCore.AddNewSession(model);
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
