using iPatient.Views;

namespace iPatient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("Facilities", typeof(FacilitiesPage));
		Routing.RegisterRoute("Facilities/Facility", typeof(FacilityEditView));
        Routing.RegisterRoute("Facilities/Facility/GenerateQRCode", typeof(GenerateQRCodePage));
        Routing.RegisterRoute("Facilities/Facility/ManageDoctors", typeof(FacilityDoctorsPage));
        Routing.RegisterRoute("Facilities/Facility/ManageDoctors/Doctor", typeof(DoctorEditPage));
        Routing.RegisterRoute("BookingOptions", typeof(BookingOptionsPage));
        Routing.RegisterRoute("BookingOptions/FindDoctor", typeof(FindDoctorPage));
        Routing.RegisterRoute("BookingOptions/FindDoctor/Doctor", typeof(DoctorVisitBookingPage));
        Routing.RegisterRoute("ScanQRCode", typeof(ScanQRCodePage));

    }
}
