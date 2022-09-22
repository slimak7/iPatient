
using iPatient.Views;

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
        public StartPageViewModel(Action<WaitingPopupPage> showWaitingPopup, string title) : base(showWaitingPopup, title)
        {
            _infoText = "To start log in or register";
            LogInCommand = new Command(() => LogIn());
            RegisterCommand = new Command(() => Register());
        }

        private void LogIn()
        {
            _showWaitingPopup(new WaitingPopupPage(async delegate() {
                await Task.Delay(5000); return true;
            }
            , null, null, "Test"));
        }

        private void Register()
        {

        }
    }
}
