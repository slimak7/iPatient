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
    public class FacilitiesViewModel : BaseViewModel<FacilitiesPage>
    {
        private bool _isNeedToLoadFacilities;
        private Facility _currentFacility;

        #region Properties

        public ObservableCollection<Facility> Facilities { get; set; }

        public Command AddNewFacilityCommand { get; set; }

        public Command<Facility> FacilityClickedCommand { get; set; }

        public Facility CurrentFacility
        {
            get => _currentFacility;
        }

        public bool IsNeedToLoadFacilities
        {
            get => _isNeedToLoadFacilities;
            set => _isNeedToLoadFacilities = value;
        }

        #endregion
        public FacilitiesViewModel(string title, FacilitiesPage viewPage) : base(title, viewPage)
        {
            AddNewFacilityCommand = new Command(() => AddUpdateFacility());

            FacilityClickedCommand = new Command<Facility>((Facility f) => FacilityClicked(f));

            Facilities = new ObservableCollection<Facility>();

            _currentFacility = null;

            IsNeedToLoadFacilities = true;
        }

        public void Load()
        {
            if (IsNeedToLoadFacilities)
            {
                LoadFacilities();
                IsNeedToLoadFacilities = false;
            }
        }

        private void LoadFacilities()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {
                var result = await APIManager.GetAllEditableFacilities();

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                Facilities.Clear();
                foreach(var facility in result.facilities)
                {
                    Facilities.Add(facility);
                }

                return true;

            }, null, null, "Pobieranie danych..."));
        }

        private async void AddUpdateFacility(Facility facility = null)
        {
            _currentFacility = facility;

            await Shell.Current.GoToAsync("NewFacility", true);
        }

        private void FacilityClicked(Facility facility)
        {
            AddUpdateFacility(facility);
        }

    }
}
