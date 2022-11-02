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
    public class FindDoctorViewModel : BaseViewModel<FindDoctorPage>
    {
        private Facility _currentFacility;
        private string _city;
        private int _selectedSpecIndex;
        private int _selectedDoctorIndex;
        private Specialization _specialization;
        private DoctorExtended _currentDoctor;
        public ObservableCollection<Specialization> Specializations { get; set; }
        public ObservableCollection<DoctorExtended> Doctors { get; set; }
        public Command SearchCommand { get; set; }
        public Command<DoctorExtended> DoctorClickedCommand { get; set; }

        public DoctorExtended CurrentDoctor
        {
            get { return _currentDoctor; }
            set { _currentDoctor = value; }
        }

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        public int SelectedSpecIndex
        {
            get { return _selectedSpecIndex; }
            set
            { 
                SetProperty(ref _selectedSpecIndex, value);
                _specialization = Specializations[_selectedSpecIndex];
            }
        }

        public FindDoctorViewModel(string title, FindDoctorPage viewPage, Facility facility, string city) : base(title, viewPage)
        {
            _currentFacility = facility;

            _city = city;

            SearchCommand = new Command(() => Search());
            DoctorClickedCommand = new Command<DoctorExtended>((DoctorExtended doctor) => DoctorClick(doctor));   

            Doctors = new ObservableCollection<DoctorExtended>();
            Specializations = new ObservableCollection<Specialization>();
        }

        public void Load()
        {
            if (Specializations.Any())
            {
                return;
            }

            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var result = await APIManager.GetAllSpecializations();

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

               
                foreach(var spec in result.specializations)
                {
                    Specializations.Add(spec);
                }

                return true;


            }, null, null, "Pobieranie danych..."));
        }

        private void Search()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var result = await APIManager.FindDoctors(_currentFacility?.Id ?? null, _specialization?.ID ?? null, City);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                Doctors.Clear();

                foreach (var doctor in result.doctors)
                {
                    doctor.Specialization.Name = Specializations.ToList().Find(x => x.ID == doctor.Specialization.ID).Name;
                    Doctors.Add(doctor);
                }

                return true;


            }, null, null, "Szukam..."));
        }

        private void DoctorClick(DoctorExtended doctor)
        {
            CurrentDoctor = doctor;
            Shell.Current.GoToAsync("BookingOptions/FindDoctor/Doctor");
        }
    }
}
