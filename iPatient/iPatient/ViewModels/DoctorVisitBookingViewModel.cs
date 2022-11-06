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
    public class DoctorVisitBookingViewModel : BaseViewModel<DoctorVisitBookingPage>
    {
        private DoctorExtended _currentDoctor;
        private DateTime _selectedDate;
        private DateTime _minDate;
        private DateTime _maxDate;

        public ObservableCollection<Visit> VisitsTime { get; set; }
        public Command ShowVisitsCommand { get; set; }
        public Command<Visit> TimeClickedCommand { get; set; }
        public string Name
        {
            get => _currentDoctor.FirstName + " " + _currentDoctor.LastName;
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public DateTime MinDate
        {
            get => _minDate;
            set => SetProperty(ref _minDate, value);
        }

        public DateTime MaxDate
        {
            get => _maxDate;
            set => SetProperty(ref _maxDate, value);
        }

        public string Specialization
        {
            get => _currentDoctor.Specialization.Name;
        }

        public DoctorVisitBookingViewModel(string title, DoctorVisitBookingPage viewPage, DoctorExtended doctor) : base(title, viewPage)
        {
            _currentDoctor = doctor;

            SelectedDate = DateTime.Today.AddDays(1).Date;
            MinDate = DateTime.Today.Date;
            MaxDate = DateTime.Today.AddDays(50);

            ShowVisitsCommand = new Command(ShowVisits);
            TimeClickedCommand = new Command<Visit>(x => TimeClicked(x));

            VisitsTime = new ObservableCollection<Visit>();
        }

        private void ShowVisits()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {

                var result = await APIManager.GetDoctorVisitsInfo(_currentDoctor.ID, SelectedDate);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                VisitsTime.Clear();

                DateTime time = result.visitsInfo.StartTime;

                while(time.TimeOfDay < result.visitsInfo.EndTime.TimeOfDay)
                {
                    VisitsTime.Add(new Visit()
                    {
                        Time = time.ToString("HH:mm"),
                        isAvailable = !result.visitsInfo.NotAvailableVisits.Contains(time.ToString("HH:mm"))
                    });

                    time = time.AddMinutes(result.visitsInfo.MinutesPerVisit);
                }

                return true;

            }, null, null, "Pobieranie danych..."));
        }

        private void TimeClicked(Visit visit)
        {
            if (!visit.isAvailable)
                return;


        }
    }
}
