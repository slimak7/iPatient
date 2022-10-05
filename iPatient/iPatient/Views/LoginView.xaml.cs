using iPatient.ViewModels;

namespace iPatient.Views;

public partial class LoginView : ContentView
{
	public LoginView()
	{
		InitializeComponent();
	}

	public LoginView(StartPageViewModel startPageViewModel)
	{
		InitializeComponent();

		BindingContext = startPageViewModel;
	}

	public void Reset()
	{
		PasswLabel.Text = EmailLabel.Text = "";
	}
}