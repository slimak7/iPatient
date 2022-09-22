using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class WaitingPopupViewModel : BaseViewModel
    {
        private Func<Task<bool>> _actionToPerform;
        private Action _actionIfSuccess;
        private Action _actionIfFailure;
        private Action _actionClosePopup;

        private string _info;

        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        public WaitingPopupViewModel(Func<Task<bool>> actionToPerform , Action actionIfSuccess, Action actionIfFailure, Action actionClosePopup) : base(null, null)
        {
            _actionToPerform = actionToPerform;
            _actionIfSuccess = actionIfSuccess;
            _actionIfFailure = actionIfFailure;
            _actionClosePopup = actionClosePopup;


            PerformAction();
        }
        public async void PerformAction()
        {
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

            _actionClosePopup();
        }
    }
}
