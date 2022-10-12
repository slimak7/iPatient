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
    public class FacilityDoctorsViewModel : BaseViewModel<FacilityDoctorsPage>
    {
        public ObservableCollection<Doctor> doctors { get; set; }

        public ObservableCollection<Specialization> specializations { get; set; }

        public bool IsNeetToLoadDoctors { get; set; }

        private Facility _currentFacility;
        public FacilityDoctorsViewModel(string title, FacilityDoctorsPage viewPage, Facility facility) : base(title, viewPage)
        {
            _currentFacility = facility;

            InstanceManager.FacilityDoctorsViewModel = this;

            IsNeetToLoadDoctors = true;
        }

        public void Load()
        {
            if (!IsNeetToLoadDoctors)
                return;

            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var result = await APIManager.GetDoctorsAndSpecializations(_currentFacility.Id);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                doctors = new ObservableCollection<Doctor>();
                specializations = new ObservableCollection<Specialization>();

                foreach(var doctor in result.doctors)
                {
                    doctors.Add(doctor);
                }

                foreach(var spec in result.specializations)
                {
                    specializations.Add(spec);
                }


                return true;

            }, () => IsNeetToLoadDoctors = false, null, "Pobieranie danych..."));
        }
    }
}
