using iPatient.ViewModels;

namespace iPatient.Views;

public partial class RegisterView : ContentView
{
	public RegisterView()
	{
		InitializeComponent();
	}

	public RegisterView(StartPageViewModel startPageViewModel)
	{
		InitializeComponent();

		BindingContext = startPageViewModel;
	}
}