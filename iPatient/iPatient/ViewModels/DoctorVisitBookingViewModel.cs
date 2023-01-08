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
        private Visit _currentVisit;
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
            MinDate = DateTime.Today.Date.AddDays(1).Date;
            MaxDate = DateTime.Today.AddDays(14);

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

                if (result.visitsInfo != null)
                {

                    DateTime time = result.visitsInfo.StartTime;

                    while (time.TimeOfDay < result.visitsInfo.EndTime.TimeOfDay)
                    {
                        VisitsTime.Add(new Visit()
                        {
                            Time = time.ToString("HH:mm"),
                            isAvailable = !result.visitsInfo.NotAvailableVisits.Contains(time.ToString("HH:mm").Trim()),
                            isUserVisit = result.visitsInfo.UserVisits.Contains(time.ToString("HH:mm").Trim())
                        });

                        time = time.AddMinutes(result.visitsInfo.MinutesPerVisit);
                    }
                }

                return true;

            }, null, null, "Pobieranie danych..."));
        }

        private void TimeClicked(Visit visit)
        {
            if (!visit.isAvailable || visit.isUserVisit)
                return;

            _currentVisit = visit;

            _viewPage.ShowPopupPage(new ConfirmPopupPage("Dzień: " + SelectedDate.ToString("dd-MM-yyyy") + "\nGodzina: " + visit.Time + "\nCzy na pewno chcesz zarezerwować?", () => BookVisit()));

        }

        private void BookVisit()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {
                string dateTime = SelectedDate.ToString("yyyy-MM-dd") + "T";
                dateTime += _currentVisit.Time;

                var result = await APIManager.BookVisit(_currentDoctor.ID, dateTime);

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }
                else
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage("Rezerwacja zakończona pomyślnie"));
                    _currentVisit.isAvailable = false;
                    return true;

                }

            }, ShowVisits, null, "Rezerwacja..."));
        }
    }
}
