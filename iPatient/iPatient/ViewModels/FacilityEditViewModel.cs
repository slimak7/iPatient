using iPatient.Managers;
using iPatient.Model;
using iPatient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class FacilityEditViewModel : BaseViewModel<FacilityEditView>
    {
        private string _facilityName;
        private string _streetName;
        private string _city;
        private string _streetNumber;
        private string _postCode;

        private Facility _facility;

        #region Properties

        public string FacilityName
        {
            get => _facilityName;
            set => SetProperty(ref _facilityName, value);
        }

        public string StreetName
        {
            get => _streetName;
            set => SetProperty(ref _streetName, value);
        }

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public string PostCode
        {
            get => _postCode;
            set => SetProperty(ref _postCode, value);
        }

        public string StreetNumber
        {
            get => _streetNumber;
            set => SetProperty(ref _streetNumber, value);
        }

        public Command SaveInfoCommand { get; set; }

        public Command ManageDoctorsCommand { get; set; }

        public Command GenerateQRCommand { get; set; }

        #endregion

        public FacilityEditViewModel(string title, FacilityEditView viewPage, Facility facility) : base(title, viewPage)
        {
            if (facility != null)
            {
                FacilityName = facility.Name;
                StreetName = facility.Address.Street;
                StreetNumber = facility.Address.StreetNumber;
                City = facility.Address.City;
                PostCode = facility.Address.PostCode;
            }

            SaveInfoCommand = new Command(() => SaveInfo());
            ManageDoctorsCommand = new Command(() => ManageDoctors());
            GenerateQRCommand = new Command(() => GenerateQR());

            _facility = facility;
        }

        private void SaveInfo()
        {
            
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {
                var result = await APIManager.UpdateFacility(new Facility()
                {
                    Id = _facility?.Id ?? "",
                    Name = _facilityName,
                    Address = new Address()
                    {
                        ID = _facility?.Address?.ID ?? "",
                        Street = _streetName,
                        StreetNumber = _streetNumber,
                        City = _city,
                        PostCode = _postCode
                    }
                });

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                return true;

            }, () => { 
                InstanceManager.FacilitiesViewModel.IsNeedToLoadFacilities = true;
                Shell.Current.SendBackButtonPressed();
            }, null, "Zapisywanie..."));
        }

        private void ManageDoctors()
        {        
            Shell.Current.GoToAsync("Facilities/Facility/ManageDoctors");            
        }

        private void GenerateQR()
        {
            Shell.Current.GoToAsync("Facilities/Facility/GenerateQRCode");
        }
    }
}
