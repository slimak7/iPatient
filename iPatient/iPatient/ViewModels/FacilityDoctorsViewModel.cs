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
        #region Properties

        public ObservableCollection<Doctor> Doctors { get; set; }

        public ObservableCollection<Specialization> Specializations { get; set; }

        public Command AddNewDoctorCommand { get; set; }

        public Command<Doctor> DoctorClickedCommand { get; set; }

        public bool IsNeedToLoadDoctors
        {
            get { return _isNeedToLoadFacilities; }
            set { _isNeedToLoadFacilities = value; }
        }

        public Doctor CurrentDoctor
        {
            get { return _currentDoctor; }
            set { _currentDoctor = value; }
        }

        #endregion

        private bool _isNeedToLoadFacilities;
        private Doctor _currentDoctor;
        private Facility _currentFacility;
        public FacilityDoctorsViewModel(string title, FacilityDoctorsPage viewPage, Facility facility) : base(title, viewPage)
        {
            _currentFacility = facility;

            CurrentDoctor = null;

            InstanceManager.FacilityDoctorsViewModel = this;

            IsNeedToLoadDoctors = true;

            Doctors = new ObservableCollection<Doctor>();

            AddNewDoctorCommand = new Command(() => AddNewDoctor());
            DoctorClickedCommand = new Command<Doctor>((Doctor doctor) => DoctorClicked(doctor));
        }

        public void Load()
        {
            if (!IsNeedToLoadDoctors)
                return;

            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var result = await APIManager.GetDoctorsAndSpecializations(_currentFacility.Id);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                Doctors.Clear();
                Specializations = new ObservableCollection<Specialization>();

                foreach(var doctor in result.doctors)
                {
                    Doctors.Add(doctor);
                }

                foreach(var spec in result.specializations)
                {
                    Specializations.Add(spec);
                }


                return true;

            }, () => IsNeedToLoadDoctors = false, null, "Pobieranie danych..."));
        }

        private void AddNewDoctor()
        {
            DoctorClicked(null);
        }

        private void DoctorClicked(Doctor doctor)
        {
            CurrentDoctor = doctor;
            Shell.Current.GoToAsync("Facilities/Facility/ManageDoctors/Doctor");
        }
    }
}
