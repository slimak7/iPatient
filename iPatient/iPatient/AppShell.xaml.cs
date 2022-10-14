using iPatient.Views;

namespace iPatient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("Facilities", typeof(FacilitiesPage));
		Routing.RegisterRoute("Facilities/Facility", typeof(FacilityEditView));
		Routing.RegisterRoute("Facilities/Facility/ManageDoctors", typeof(FacilityDoctorsPage));
        Routing.RegisterRoute("Facilities/Facility/ManageDoctors/Doctor", typeof(DoctorEditPage));

    }
}
