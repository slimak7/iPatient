using iPatient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class GenerateQRCodeViewModel : BaseViewModel<GenerateQRCodePage>
    {
        private string _code;

        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); } 
        }
        public GenerateQRCodeViewModel(string title, GenerateQRCodePage viewPage, string facilityID) : base(title, viewPage)
        {
            _code = facilityID;
        }


    }
}
