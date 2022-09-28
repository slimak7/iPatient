
using iPatient.Helpers;
using iPatient.Managers;
using iPatient.ReqModels;
using iPatient.Views;
using Microsoft.Maui.ApplicationModel.Communication;

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
            string validationMessage = "";

            switch(_selectedOption)
            {
                case SelectedOption.login:

                    LoginReq loginReq = new LoginReq();

                    if (ValidateData(ref validationMessage))
                    {
                        loginReq.EmailAddress = Email;
                        loginReq.Password = Password;
                    }
                    else
                    {
                        _viewPage.ShowPopupPage(new InfoPopupPage(validationMessage));
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

                    RegisterReq registerReq = new RegisterReq();

                    if (ValidateData(ref validationMessage))
                    {
                        registerReq.Email = Email;
                        registerReq.Password = Password;
                        registerReq.FirstName = FirstName;
                        registerReq.LastName = LastName;
                    }
                    else
                    {
                        _viewPage.ShowPopupPage(new InfoPopupPage(validationMessage));
                        return;
                    }

                    _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
                    {

                        var result = await APIManager.Register(registerReq);

                        if (!result.OK)
                        {
                            _viewPage.ShowPopupPage(new InfoPopupPage(result.Errors));
                        }

                        return result.OK;

                    }, null, null, "Registration..."));

                break;
            }
        }

        public override bool ValidateData(ref string message)
        {
            switch (_selectedOption) 
            {

                case SelectedOption.login:

                    if (!DataValidation.ValidateEmail(Email))
                    {
                        message = "Invalid email address";
                        return false;
                    }
                    if (!DataValidation.ValidatePassword(Password))
                    {
                        message = "Password requirements: min 8. chars, min. 1 number and upper char";
                        return false;
                    }


                    break;

                case SelectedOption.register:

                    if (!DataValidation.ValidateEmail(Email))
                    {
                        message = "Invalid email address";
                        return false;
                    }
                    if (!DataValidation.ValidatePassword(Password))
                    {
                        message = "Password requirements: min 8. chars, min. 1 number and upper char";
                        return false;
                    }
                    if (Password != ConfirmPassword)
                    {
                        message = "Passwords do not match";
                        return false;
                    }
                    if (FirstName == null || FirstName == "")
                    {
                        message = "First name can not be empty";
                        return false;
                    }
                    if (LastName == null || LastName == "")
                    {
                        message = "Last name can not be empty";
                        return false;
                    }

                break;

            }   
            return true;
        }
    }
}
