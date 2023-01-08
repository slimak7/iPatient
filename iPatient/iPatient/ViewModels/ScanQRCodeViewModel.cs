using iPatient.Managers;
using iPatient.Model;
using iPatient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class ScanQRCodeViewModel : BaseViewModel<ScanQRCodePage>
    {
        private string _facilityID;
        private VisitReceived _visit;
        public ScanQRCodeViewModel(string title, ScanQRCodePage viewPage) : base(title, viewPage)
        {
        }

        public void QRCodeReaded(string facilityID)
        {
            _facilityID = facilityID;

            if (facilityID != null)
            {
                _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
                {

                    var result = await APIManager.ReceiveVisit(_facilityID);

                    if (!result.ok)
                    {
                        _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                        return false;
                    }

                    _visit = result.visit;

                    return true;

                }, () => _viewPage.ShowPopupPage(new ConfirmPopupPage(
                    _visit.DoctorName + "\nGodzina: " + _visit.Time.Substring(0, 5) + "\nPiętro: " + _visit.FloorLevel + ", gabinet nr: " + _visit.OfficeNumber, ConfirmVisit)), null, "Pobieranie danych..."));
            }
        }

        private void ConfirmVisit()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var result = await APIManager.ConfirmReceivedVisit(_visit.VisitID);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }
                else
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage("Potwierdzono ;)"));
                    return true;
                }


            }, null, null, "Potwierdzanie odebrania wizyty"));
        }
    } 
}
