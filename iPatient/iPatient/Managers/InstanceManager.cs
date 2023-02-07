using iPatient.Model;
using iPatient.ViewModels;


namespace iPatient.Managers
{
    public static class InstanceManager
    {
        public static StartPageViewModel StartPageViewModel { get; set; }
        public static FacilitiesViewModel FacilitiesViewModel { get; set; }
        public static FacilityDoctorsViewModel FacilityDoctorsViewModel { get; set; }
        public static FindDoctorViewModel FindDoctorViewModel { get; set; }
        public static Dictionaries.Dictionary.UserRoles.Roles CurrentUserRole { get; set; }
        public static Facility CurrentSelectedFacility { get; set; }
    }
}
