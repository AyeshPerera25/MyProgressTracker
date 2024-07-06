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
        public IActionResult CoursesView()
        {
            CourseResponse response = _systemServiceCore.GetAllCourses();
            if (response != null)
            {
                TempData["message"] = response?.Description ?? "Unable to load users";

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
        public IActionResult SubjectsView()
        {
            return View();
        }
        public IActionResult StudySessionsView()
        {
            return View();
        }

        public IActionResult DashboardView()
        {
            return View();
        }
        public IActionResult AddCourseView()
        {
            return View();
        }

    }
}
