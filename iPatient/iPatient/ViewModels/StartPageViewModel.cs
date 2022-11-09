
using iPatient.Helpers;
using iPatient.Managers;
using iPatient.Model;
using iPatient.ReqModels;
using iPatient.Views;


namespace iPatient.ViewModels
{
    public class StartPageViewModel : BaseViewModel<StartPage>
    {
        private string _infoText;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _PESEL;

        private User _currentUser;
        private Address _address;

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

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public string PESEL
        {
            get { return _PESEL; }
            set { SetProperty(ref _PESEL, value); }
        }

        public string City
        {
            get => _address?.City ?? null;
        }

        public Command LogInCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public Command ContinueCommand { get; set; }

        public Command EditUserInfoCommand { get; set; }
        public Command SaveUserInfoCommand { get; set; }

        public Command ExpandCollapseUserInfoCommand { get; set; }

        public Command ShowFacilitiesCommand { get; set; }
        public Command LogOutCommand { get; set; }

        public Command BookVisitCommand { get; set; }

        public Command ScanQRCodeCommand { get; set; }

        #endregion

        public StartPageViewModel(string title, StartPage startPage) : base(title, startPage)
        {
            _infoText = "Aby rozpocząć zaloguj się lub zarejestruj";
            LogInCommand = new Command(() => SelectLogIn());
            RegisterCommand = new Command(() => SelectRegister());
            ContinueCommand = new Command(() => Continue());
            EditUserInfoCommand = new Command(() => EditUserData());
            SaveUserInfoCommand = new Command(() => SaveUserData());
            ExpandCollapseUserInfoCommand = new Command(() => ExpandeCollapseUserInfo());
            ShowFacilitiesCommand = new Command(() => ShowFacilities());
            LogOutCommand = new Command(() => LogOut());
            BookVisitCommand = new Command(() => BookVisit());
            ScanQRCodeCommand = new Command(() => ScanQRCode());

            SelectLogIn();

            AutoLogin();
        }

        private async void AutoLogin()
        {
            var resultEmail = await Xamarin.Essentials.SecureStorage.GetAsync("email");

            if (resultEmail == null)
            {
                return;
            }

            var resultPassw = await Xamarin.Essentials.SecureStorage.GetAsync("password");

            if (resultPassw == null)
            {
                return;
            }
            
            await Task.Delay(500);
            
            Email = resultEmail;
            Password = resultPassw;

            Continue();
        }

        private void SelectLogIn()
        {
            _selectedOption = SelectedOption.login;

            _viewPage.SetLoginButton(true);
            _viewPage.SetRegisterButton(false);
        }

        private void SelectRegister()
        {
            _selectedOption = SelectedOption.register;

            _viewPage.SetLoginButton(false);
            _viewPage.SetRegisterButton(true);
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
                        else
                        {
                            await Xamarin.Essentials.SecureStorage.SetAsync("email", Email);
                            await Xamarin.Essentials.SecureStorage.SetAsync("password", Password);
                        }

                        return result.OK;

                    }, LoadUserInfo, null, "Logowanie..."));



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
                        else
                        {
                            await Xamarin.Essentials.SecureStorage.SetAsync("email", Email);
                            await Xamarin.Essentials.SecureStorage.SetAsync("password", Password);
                        }

                        return result.OK;

                    }, LoadUserInfo, null, "Rejestracja..."));

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
                        message = "Niepoprawny adres email";
                        return false;
                    }
                    /*
                    if (!DataValidation.ValidatePassword(Password))
                    {
                        message = "Wymagania hasła: min. 8 znaków, co najmniej jedna duża litera, co najmniej jedna cyfra";
                        return false;
                    }
                    */

                    break;

                case SelectedOption.register:

                    if (!DataValidation.ValidateEmail(Email))
                    {
                        message = "Niepoprawny adres email";
                        return false;
                    }
                    if (!DataValidation.ValidatePassword(Password))
                    {
                        message = "Wymagania hasła: min. 8 znaków, co najmniej jedna duża litera, co najmniej jedna cyfra";
                        return false;
                    }
                    if (Password != ConfirmPassword)
                    {
                        message = "Hasła nie zgadzają się";
                        return false;
                    }
                    if (FirstName == null || FirstName == "")
                    {
                        message = "Imię nie może być puste";
                        return false;
                    }
                    if (LastName == null || LastName == "")
                    {
                        message = "Nazwisko nie może być puste";
                        return false;
                    }

                break;

            }   
            return true;
        }

        private void LoadUserInfo()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var userResponse = await APIManager.GetCurrentUserInfo();

                if (!userResponse.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(userResponse.errors));
                    return false;
                }
                else
                {
                    _currentUser = userResponse.user;

                    var addressResponse = await APIManager.GetCurrentUserAddress();

                    if (!addressResponse.ok)
                    {
                        _viewPage.ShowPopupPage(new InfoPopupPage(addressResponse.errors));
                        return false;
                    }

                    _address = addressResponse.address;
                }

                return true;

            }, ProcessUserInfo, null, "Pobieranie informacji o koncie..."));
        }

        private void ProcessUserInfo()
        {
            InstanceManager.CurrentUserRole = _currentUser.UserRole;

            _viewPage.ShowUserData(_currentUser, _address);
            _viewPage.SetMenuButtons(_currentUser.UserRole);

            FirstName = _currentUser.FirstName;
            LastName = _currentUser.LastName;
            Email = _currentUser.Email;
            Phone = _currentUser.PhoneNumber;
            PESEL = _currentUser.PESEL;
        }

        private void EditUserData()
        {
            _viewPage.EditUserData();
        }

        private void SaveUserData()
        {
            if (!_viewPage.IsReadyToSave())
            {
                _viewPage.ShowPopupPage(new InfoPopupPage("Brak zmian do zapisu"));
                return;
            }

            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var data = _viewPage.GetUserInfo();

                var userInfoResponse = await APIManager.UpdateUserInfo(
                    new User()
                    {
                        PhoneNumber = data.user.PhoneNumber,
                        PESEL = data.user.PESEL
                    });

                if (!userInfoResponse.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(userInfoResponse.errors));
                    return false;
                }
                else
                {

                    var addressResponse = await APIManager.UpdateUserAddress(new Address()
                    {
                        Street = data.address.Street,
                        StreetNumber = data.address.StreetNumber,
                        City = data.address.City,
                        PostCode = data.address.PostCode
                    });

                    if (!addressResponse.ok)
                    {
                        _viewPage.ShowPopupPage(new InfoPopupPage(addressResponse.errors));
                        return false;
                    }

                }

                _currentUser = data.user;
                _address = data.address;

                return true;

            }, _viewPage.UserInfoSavedSuccess, _viewPage.UserInfoSavedNoSuccess, "Zapisywanie zmian..."));
        }

        private void ExpandeCollapseUserInfo()
        {
            _viewPage.ExpandCollapseUserInfo();
        }

        private void ShowFacilities()
        {
            Shell.Current.GoToAsync("Facilities", true);
        }

        private void LogOut()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                Xamarin.Essentials.SecureStorage.Remove("password");
                Xamarin.Essentials.SecureStorage.Remove("email");

                ResetVariables();

                _selectedOption = SelectedOption.login;

                _viewPage.LogOut();

                return true;

            }, null, null, "Wylogowywanie..."));
        }

        private void ResetVariables()
        {
            _currentUser = _currentUser = null;
            FirstName = LastName = Password = Email = Phone = Password = ConfirmPassword
                = PESEL = null;
        }

        private void BookVisit()
        {
            Shell.Current.GoToAsync("BookingOptions");
        }

        private void ScanQRCode() 
        { 

        }
    }
}
