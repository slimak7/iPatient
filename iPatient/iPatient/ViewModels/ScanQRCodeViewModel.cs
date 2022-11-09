using iPatient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class ScanQRCodeViewModel : BaseViewModel<ScanQRCodePage>
    {
        private string _facilityID;
        public ScanQRCodeViewModel(string title, ScanQRCodePage viewPage) : base(title, viewPage)
        {
        }

        public void QRCodeReaded(string facilityID)
        {
            _facilityID = facilityID;
        }
    }
}
