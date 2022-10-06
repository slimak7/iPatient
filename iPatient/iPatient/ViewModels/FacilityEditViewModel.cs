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
        }

        private void SaveInfo()
        {

        }
    }
}
