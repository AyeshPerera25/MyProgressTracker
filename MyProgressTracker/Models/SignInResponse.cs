using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Models
{
    public class SignInResponse : ResponseWrapper
	{
		public SiginInViewModel? SignInViewModel { get; set; }
		public User? User { get; set; }
		
	}
}
