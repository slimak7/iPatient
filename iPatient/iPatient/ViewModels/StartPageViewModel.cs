namespace iPatient.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        private string _infoText;
        public string InfoText
        {
            get { return _infoText; }
            set { SetProperty(ref _infoText, value); }
        }

        public Command LogInCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public StartPageViewModel()
        {
            Title = "Welcome to iPatient!";
            _infoText = "To start log in or register";
            LogInCommand = new Command(() => LogIn());
            RegisterCommand = new Command(() => Register());
        }

        private void LogIn()
        {

        }

        private void Register()
        {

        }
    }
}
