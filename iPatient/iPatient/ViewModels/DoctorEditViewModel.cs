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
    public class DoctorEditViewModel : BaseViewModel<DoctorEditPage>
    {
        #region Properties

        public Command SaveInfoCommand { get; set; }

        public string FirstName
        {
            get { return _firstName; }
            set {  SetProperty(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public string OfficeNumber
        {
            get { return _officeNumber; }
            set { SetProperty(ref _officeNumber, value); }
        }

        public string FloorNumber
        {
            get { return _floorNumber; }
            set { SetProperty(ref _floorNumber, value); }
        }

        public string SpecName
        {
            get { return _specName; }
            set { SetProperty(ref _specName, value); }
        }

        public string SpecID
        {
            get { return _specID; }
            set { SetProperty(ref _specID, value); }
        }

        public int SelectedSpecIndex
        {
            get { return _selectedSpecIndex; }
            set
            { 
                SetProperty(ref _selectedSpecIndex, value);

                if (_selectedSpecIndex >= 0 && _selectedSpecIndex < Specializations.Count)
                {
                    var currentSpec = Specializations[_selectedSpecIndex];
                    SpecID = currentSpec.ID;
                    SpecName = currentSpec.Name;
                }
            }
        }

        public ObservableCollection<Specialization> Specializations { get; set; }

        #endregion

        private Doctor _doctor;
        private Facility _facility;

        private string _firstName;
        private string _lastName;
        private string _officeNumber;
        private string _floorNumber;
        private string _specName;
        private string _specID;
        private int _selectedSpecIndex;

        public DoctorEditViewModel(string title, DoctorEditPage viewPage, Facility facility, Doctor doctor, ObservableCollection<Specialization> specializations) : base(title, viewPage)
        {
            _facility = facility;
            _doctor = doctor;
            Specializations = specializations;

            if (doctor != null)
            {
                FirstName = doctor.FirstName;
                LastName = doctor.LastName;
                OfficeNumber = doctor.OfficeNumber;
                FloorNumber = doctor.FloorNumber;

                SelectedSpecIndex = Specializations.IndexOf(Specializations.Where(x => x.ID == doctor.Specialization.ID).FirstOrDefault());

            }

            SaveInfoCommand = new Command(() => SaveInfo());
           
        }

        private void SaveInfo()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {
                var result = await APIManager.UpdateDoctor(new Doctor()
                {
                    ID = _doctor?.ID ?? "",
                    FirstName = _firstName,
                    LastName = _lastName,
                    OfficeNumber = _officeNumber,
                    FloorNumber = _floorNumber,
                    Specialization = new Specialization()
                    {
                        ID = SpecID,
                        Name = SpecName
                    }
                }, _facility.Id);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                return true;

            }, () => {
                InstanceManager.FacilityDoctorsViewModel.IsNeedToLoadDoctors = true;
                Shell.Current.SendBackButtonPressed();
            }, null, "Zapisywanie..."));
        }

    }
}
