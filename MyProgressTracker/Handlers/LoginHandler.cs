using MyProgressTracker.Models;
using MyProgressTracker.ServiceConnectors;
using MyProgressTrackerAuthenticationService.Models.DataTransferObjects;

namespace MyProgressTracker.Handlers
{
    public class LoginHandler
    {
        private AthenticationServiceConnector _authServiceConnector;

        public LoginHandler(AthenticationServiceConnector authServiceConnector)
        {
            _authServiceConnector = authServiceConnector;
        }

        public LoginResponse UserLogin(LoginViewModel loginViewModel)
        {
            validateLoginViewModel(loginViewModel);
            UserLoginReq request = populateUserLoginReq(loginViewModel);
            UserLoginRes userLoginResponse = _authServiceConnector.UserLoginAsync(request).GetAwaiter().GetResult();
            validateUserLoginRes(userLoginResponse);
            LoginResponse response = populateLoginResponse(userLoginResponse);
            response.LoginViewModel = loginViewModel;
            return response;
        }

        private void validateUserLoginRes(UserLoginRes? response)
        {
            if (response != null)
            {
                if (!response.IsRequestSuccess)
                {
                    throw new Exception("User Login Req has failed due to: " + response.Description);
                }
            }
            else
            {
                throw new Exception("User Login Res not found! ");
            }
        }

        private UserLoginReq populateUserLoginReq(LoginViewModel loginViewModel)
        {
            UserLoginReq loginReq = new UserLoginReq();
            loginReq.Email = loginViewModel.Email;
            loginReq.Password = loginViewModel.Password;
            return loginReq;
        }

        private void validateLoginViewModel(LoginViewModel viewModel)
        {
            if (viewModel.Email == null || viewModel.Email == string.Empty)
            {
                throw new Exception("The Email has not enterd!");
            }
            if (viewModel.Password == null || viewModel.Password == string.Empty)
            {
                throw new Exception("The Password has not enterd!");
            }
        }

        private LoginResponse populateLoginResponse(UserLoginRes userLoginResponse)
        {
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.UserId = userLoginResponse.UserId;
            loginResponse.UserName = userLoginResponse.UserFirstName+ " " + userLoginResponse.UserLastName;
            loginResponse.SessionKey = userLoginResponse.SessionKey;
            loginResponse.IsRequestSuccess = userLoginResponse.IsRequestSuccess;
            loginResponse.Description  = userLoginResponse.Description;
            return loginResponse;
        }
    }
}
