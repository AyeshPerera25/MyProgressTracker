using MyProgressTracker.Handlers;
using MyProgressTracker.Models;

namespace MyProgressTracker.ServiceCore
{
	public class SystemServiceCore
	{
		private readonly SignInHandler _signInHandler;
        private readonly CourseHandler _courseHandler;

        public SystemServiceCore(SignInHandler signInHandler, CourseHandler courseHandler)
        {
            _signInHandler = signInHandler;
            _courseHandler = courseHandler;
        }

		public SignInResponse proccessSignIn(SiginInViewModel signInViewModel)
        {
			SignInResponse signInResponse = null;
			try
            {
				signInResponse = _signInHandler.SignIn(signInViewModel);
            }
            catch (Exception ex)
            {
				signInResponse = new SignInResponse();
				signInResponse.IsRequestSuccess = false;
				signInResponse.Description = ex.Message;
			}
			return signInResponse;

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



    }
}
