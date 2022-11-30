using iPatient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class BookingOptionsViewModel : BaseViewModel<BookingOptionsPage>
    {
        public Command FindDoctorsCommand { get; set; }
        public Command FindDoctorsByFacilityCommand { get; set; }

        public BookingOptionsViewModel(string title, BookingOptionsPage viewPage) : base(title, viewPage)
        {
            FindDoctorsCommand = new Command(() => FindDoctors());
            FindDoctorsByFacilityCommand = new Command(() => FindDoctorsByFacility());
        }

        private void FindDoctors()
        {
            Shell.Current.GoToAsync("BookingOptions/FindDoctor");
        }

        private void FindDoctorsByFacility()
        {
            Shell.Current.GoToAsync("BookingOptions/FindFacility");
        }
    }
}
