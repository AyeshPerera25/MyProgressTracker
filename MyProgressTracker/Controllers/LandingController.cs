using Microsoft.AspNetCore.Mvc;

namespace MyProgressTracker.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult LandingView()
        {
            return View();
        }
    }
}
