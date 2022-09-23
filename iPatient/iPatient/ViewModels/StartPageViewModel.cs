
using iPatient.Views;

namespace iPatient.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        private string _infoText;
        private StartPage _startPage;
        private string _login;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _username;
        private string _firstName;
        private string _lastName;

        #region Properties

        public string InfoText
        {
            get { return _infoText; }
            set { SetProperty(ref _infoText, value); }
        }

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        #endregion

        public Command LogInCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public StartPageViewModel(Action<WaitingPopupPage> showWaitingPopup, string title, StartPage startPage) : base(showWaitingPopup, title)
        {
            _infoText = "To start log in or register";
            LogInCommand = new Command(() => LogIn());
            RegisterCommand = new Command(() => Register());
            _startPage = startPage;

            LogIn();
        }

        private void LogIn()
        {
            _startPage.SetLoginButton(true);
            _startPage.SetRegisterButton(false);
        }

        private void Register()
        {
            _startPage.SetLoginButton(false);
            _startPage.SetRegisterButton(true);
        }
    }
}
