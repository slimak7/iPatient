﻿using iPatient.Model;
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

        public ObservableCollection<DateTime> VisitsTime { get; set; }
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
        }
    }
}
