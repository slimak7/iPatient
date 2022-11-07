using iPatient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class ConfirmPopupViewModel : BaseViewModel<ConfirmPopupPage>
    {
        private Action _actionToPerform;

        public Command ContinueCommand { get; set; }
        public Command CancelCommand { get; set; }

        public ConfirmPopupViewModel(string title, ConfirmPopupPage viewPage, Action actionToPerform) : base(title, viewPage)
        {
            _actionToPerform = actionToPerform;

            ContinueCommand = new Command(() => Continue());
            CancelCommand = new Command(() => Cancel());
        }

        private void Continue()
        {
            _actionToPerform();
            Cancel();
        }

        private void Cancel()
        {
            _viewPage.Close();
        }
    }
}
