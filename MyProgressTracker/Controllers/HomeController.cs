using Microsoft.AspNetCore.Mvc;
using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using System.Diagnostics;

namespace MyProgressTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InMemoryDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, InMemoryDBContext inMemoryDB)
        {
            _logger = logger;
            _dbContext = inMemoryDB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
