
using iPatient.Helpers;
using iPatient.Managers;
using iPatient.ReqModels;
using iPatient.Views;

namespace iPatient.ViewModels
{
    public class StartPageViewModel : BaseViewModel<StartPage>
    {
        private string _infoText;
        private StartPage _startPage;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _firstName;
        private string _lastName;

        private enum SelectedOption
        {
            login, register
        };

        private SelectedOption _selectedOption;

        #region Properties

        public string InfoText
        {
            get { return _infoText; }
            set { SetProperty(ref _infoText, value); }
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

        public Command LogInCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public Command ContinueCommand { get; set; }

        #endregion

        public StartPageViewModel(string title, StartPage startPage) : base(title, startPage)
        {
            _infoText = "To start log in or register";
            LogInCommand = new Command(() => LogIn());
            RegisterCommand = new Command(() => Register());
            ContinueCommand = new Command(() => Continue());
            _startPage = startPage;

            LogIn();
        }

        private void LogIn()
        {
            _selectedOption = SelectedOption.login;

            _startPage.SetLoginButton(true);
            _startPage.SetRegisterButton(false);
        }

        private void Register()
        {
            _selectedOption = SelectedOption.register;

            _startPage.SetLoginButton(false);
            _startPage.SetRegisterButton(true);
        }

        private void Continue()
        {
            switch(_selectedOption)
            {
                case SelectedOption.login:

                    LoginReq loginReq = new LoginReq();

                    loginReq.Password = Password;

                    if (DataValidation.ValidateEmail(Email))
                    {
                        loginReq.EmailAddress = Email;
                    }
                    else
                    {
                        return;
                    }

                    _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
                    {

                        var result = await APIManager.Login(loginReq);

                        if (!result.OK)
                        {
                            _viewPage.ShowPopupPage(new InfoPopupPage(result.Errors));
                        }

                        return result.OK;

                    }, null, null, "Logging in..."));



                    break;

                case SelectedOption.register:

                    break;
            }
        }
    }
}
