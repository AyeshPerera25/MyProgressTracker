namespace MyProgressTracker.Models
{
    public class LoginResponse : ResponseWrapper
    {
        public string SessionKey { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public LoginViewModel LoginViewModel { get; set; }   
    }
}
