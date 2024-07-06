using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Handlers
{
    public class SignInHandler
	{
		private readonly InMemoryDBContext _inMemoryDB;

        public SignInHandler(InMemoryDBContext context)
        {
            _inMemoryDB = context;
        }

        public SignInResponse SignIn(SiginInViewModel viewModel)
        {
            SignInResponse signInResponse = null;
            User? user = null;

       
            validateSignInModel(viewModel);
            user = populateUser(viewModel);
            persistUser(user);

            signInResponse = new SignInResponse();
            signInResponse.IsRequestSuccess = true;
            signInResponse.Description = "Success!";
            signInResponse.SignInViewModel = viewModel;
            signInResponse.User = user;
            
            return signInResponse;
		}

		private void persistUser(User? user)
		{
            if (user != null)
            {
                _inMemoryDB.Users.Add(user);
                _inMemoryDB.SaveChanges();
            } else
            {
				throw new Exception("Null User Entity to persist" );
			}

		}

		private User populateUser(SiginInViewModel viewModel)
		{
			DateTime localTime = DateTime.Now;
			long id = long.Parse(localTime.ToString("yyyyMMddHHmmss"));
            return new User(id, viewModel.FirstName, viewModel.LastName, viewModel.Email, viewModel.Password);
		}

		private void validateSignInModel(SiginInViewModel viewModel)
		{
            User? user = null;
            

            if (viewModel.Password != viewModel.ConfirmPassword)
            {
				throw new Exception("The password and confirmation password do not match!");
			}
            if (_inMemoryDB.Users.Any())
            {
                user = _inMemoryDB.Users.SingleOrDefault<User>(user => user.Email == viewModel.Email);
				if (user != null)
				{
					throw new Exception("User already registerd under the email. Try another!");
				}
			}

            
		}
	}
}
