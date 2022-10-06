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

        #region Properties

        public ObservableCollection<Facility> Facilities { get; set; }

        public Command AddNewFacilityCommand { get; set; }

        #endregion
        public FacilitiesViewModel(string title, FacilitiesPage viewPage) : base(title, viewPage)
        {
            AddNewFacilityCommand = new Command(() => AddNewFacility());

            Facilities = new ObservableCollection<Facility>();

            LoadFacilities();
        }

        private async void LoadFacilities()
        {
            await Task.Delay(500);

            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {
                var result = await APIManager.GetAllEditableFacilities();

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                foreach(var facility in result.facilities)
                {
                    Facilities.Add(facility);
                }

                return true;

            }, null, null, "Pobieranie danych..."));
        }

        private async void AddNewFacility()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("facility", null);

            await Shell.Current.GoToAsync("NewFacility", true, parameters);
        }
    }
}
