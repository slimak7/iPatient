using iPatient.Views;

namespace iPatient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("Facilities", typeof(FacilitiesPage));

    }
}
