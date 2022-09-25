using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Views
{
    public interface ViewBase
    {
        public void ShowPopupPage(WaitingPopupPage popupPage);

        public void ShowPopupPage(InfoPopupPage infoPopupPage);
        
    }
}
