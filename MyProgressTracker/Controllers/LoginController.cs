using Microsoft.AspNetCore.Mvc;
using MyProgressTracker.Models;
using MyProgressTracker.ServiceCore;
using System.Diagnostics;

namespace MyProgressTracker.Controllers
{
    public class LoginController : Controller
    {
        private readonly SystemServiceCore _systemServiceCore;

        public LoginController(SystemServiceCore systemService)
        {
			_systemServiceCore = systemService;
        }

        public IActionResult UserLogin()
        {
			CompositLoginViewModel compositLoginView = new CompositLoginViewModel();
			compositLoginView.IsLoggedIn = 1;
			return View(compositLoginView);
        }

		public IActionResult SignIn(CompositLoginViewModel compositLoginView)
		{
            SignInResponse signInResponse = null;
			if (compositLoginView != null)
            {
				signInResponse = _systemServiceCore.proccessSignIn(compositLoginView.SiginInViewModel);
			}
			TempData["message"] = signInResponse?.Description ?? "Invalid login attempt";

            if (signInResponse.IsRequestSuccess)
            {
				TempData["messageTyp"] = "S"; // Success
				compositLoginView.IsLoggedIn = 1;
			}
			else
            {
				TempData["messageTyp"] = "E"; // Error
				compositLoginView.IsLoggedIn = 0;
			}
            

			return View("UserLogin", compositLoginView);
		}

        public IActionResult Login(CompositLoginViewModel compositLoginView)
        {
            LoginResponse loginResponse = null;
            if (compositLoginView != null)
            {
                loginResponse = _systemServiceCore.proccessLogin(compositLoginView.LoginViewModel);
            }
            TempData["message"] = loginResponse?.Description ?? "Invalid login attempt";

            if (loginResponse.IsRequestSuccess)
            {
                TempData["messageTyp"] = "S"; // Success
                compositLoginView.IsLoggedIn = 1;
                HttpContext.Session.SetString(SystemConstant.SessionKey, loginResponse.SessionKey);
                HttpContext.Session.SetString(SystemConstant.UserName, loginResponse.UserName);
                HttpContext.Session.SetString(SystemConstant.UserID, string.Concat(loginResponse.UserId));
				return RedirectToAction("DashboardView", "Dashboard");
			}
            else
            {
                TempData["messageTyp"] = "E"; // Error
                compositLoginView.IsLoggedIn = 0;
            }
            return View("UserLogin", compositLoginView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
