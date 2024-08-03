using MyProgressTracker.DataResources;
using MyProgressTracker.ServiceConnectors;
using MyProgressTracker.Models;
using MyProgressTracker.Models.Entity;
using MyProgressTrackerAuthenticationService.Models.DataTransferObjects;

namespace MyProgressTracker.Handlers
{
    public class SignInHandler
	{
        private AthenticationServiceConnector _authServiceConnector;

        public SignInHandler(AthenticationServiceConnector authServiceConnector)
        {
            _authServiceConnector = authServiceConnector;
        }

        public SignInResponse NewUserRegistration(SiginInViewModel viewModel)
        {
            validateSignInModel(viewModel);
            NewUserRegistrationReq request = getNewUserRegistrationRequest(viewModel);
            NewUserRegistrationRes response = _authServiceConnector.UserRegisterAsync(request).GetAwaiter().GetResult();
            validateUserRegistrationResponse(response);
            SignInResponse signInResponse =  populateSignInResponse(response);
            signInResponse.SignInViewModel = viewModel;
            return signInResponse;
        }

        private SignInResponse populateSignInResponse(NewUserRegistrationRes response)
        {
            SignInResponse signInResponse = new SignInResponse();
            signInResponse.IsRequestSuccess = response.IsRequestSuccess;
            signInResponse.Description = response.Description; 
            return signInResponse;
        }

        private void validateUserRegistrationResponse(NewUserRegistrationRes response)
        {
            if (response != null)
            {
                if (!response.IsRequestSuccess)
                {
                    throw new Exception("New User Registration Req has failed due to: " + response.Description);
                }
            }
            else
            {
                throw new Exception("New User Registration Res not found! ");
            }
        }

        private NewUserRegistrationReq getNewUserRegistrationRequest(SiginInViewModel viewModel)
        {
            NewUserRegistrationReq req = new NewUserRegistrationReq();
            req.FirstName = viewModel.FirstName;
            req.LastName = viewModel.LastName;
            req.Email = viewModel.Email;
            req.Password = viewModel.Password;
            req.ConfirmPassword = viewModel.ConfirmPassword;
            return req;
        }

        private void validateSignInModel(SiginInViewModel viewModel)
        {
            if(viewModel.Email == null ||  viewModel.Email == string.Empty)
            {
                throw new Exception("The Email has not enterd!");
            }
            if (viewModel.FirstName == null || viewModel.FirstName == string.Empty)
            {
                throw new Exception("The First Name has not enterd!");
            }
            if (viewModel.LastName == null || viewModel.LastName == string.Empty)
            {
                throw new Exception("The Last Name has not enterd!");
            }
            if (viewModel.Password == null || viewModel.Password == string.Empty)
            {
                throw new Exception("The Password has not enterd!");
            }
            if (viewModel.ConfirmPassword == null || viewModel.ConfirmPassword == string.Empty)
            {
                throw new Exception("The Confirm Password has not enterd!");
            }
            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                throw new Exception("The password and confirmation password do not match!");
            }
        }

		private User populateUser(SiginInViewModel viewModel)
		{
			DateTime localTime = DateTime.Now;
			long id = long.Parse(localTime.ToString("yyyyMMddHHmmss"));
            return new User(id, viewModel.FirstName, viewModel.LastName, viewModel.Email, viewModel.Password);
		}
	}
}
