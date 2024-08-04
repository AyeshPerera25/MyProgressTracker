using MyProgressTrackerDependanciesLib.Models.Entities;

namespace MyProgressTracker.Models
{
    public class SignInResponse : ResponseWrapper
	{
		public SiginInViewModel? SignInViewModel { get; set; }
		public User? User { get; set; }
		
	}
}
