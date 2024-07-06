namespace MyProgressTracker.Models
{
	public class CompositLoginViewModel
	{
		public LoginViewModel LoginViewModel { get; set; }
		public SiginInViewModel SiginInViewModel { get; set; }
		public int IsLoggedIn { get; set; } = 1;

        public CompositLoginViewModel()
        {
            LoginViewModel = new LoginViewModel();
			SiginInViewModel = new SiginInViewModel();
        }
    }
}
