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
    public class FindFacilityViewModel : BaseViewModel<FindFacilityPage>
    {
        private string _city;

        public string City { get { return _city; } set { SetProperty(ref _city, value); } }

        public ObservableCollection<Facility> Facilities { get; set; }

        public Command SearchCommand { get; set; }
        public Command<Facility> FacilityClickedCommand { get; set; }

        public FindFacilityViewModel(string title, FindFacilityPage viewPage, string city) : base(title, viewPage)
        {
            SearchCommand = new Command(() => Search());
            FacilityClickedCommand = new Command<Facility>(FacilityClicked);

            Facilities = new ObservableCollection<Facility>();
            City = city;
        }

        private void Search()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var result = await APIManager.GetAllFacilitiesByCity(City);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                Facilities.Clear();

                foreach (var facility in result.facilities)
                {
                    Facilities.Add(facility);
                }

                return true;


            }, null, null, "Szukam..."));
        }

        private void FacilityClicked(Facility facility)
        {
            InstanceManager.CurrentSelectedFacility = facility;

            Shell.Current.GoToAsync("BookingOptions/FindFacility/FindDoctor");
        }
    }
}
