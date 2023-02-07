using iPatient.Views;


namespace iPatient.ViewModels
{
    public class WaitingPopupViewModel : BaseViewModel<WaitingPopupPage>
    {
        private Func<Task<bool>> _actionToPerform;
        private Action _actionIfSuccess;
        private Action _actionIfFailure;

        private string _info;

        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        public WaitingPopupViewModel(Func<Task<bool>> actionToPerform , Action actionIfSuccess, Action actionIfFailure, WaitingPopupPage waitingPopupPage) : base(null, waitingPopupPage)
        {
            _actionToPerform = actionToPerform;
            _actionIfSuccess = actionIfSuccess;
            _actionIfFailure = actionIfFailure;

            PerformAction();
        }
        public async void PerformAction()
        {
            await Task.Delay(600);

            bool result = await _actionToPerform();

            if (result)
            {
                if (_actionIfSuccess != null)
                    _actionIfSuccess();
            }
            else
            {
                if (_actionIfFailure != null)
                    _actionIfFailure();
            }

            _viewPage.Close();
        }
    }
}
