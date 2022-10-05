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

	public void Reset()
	{
		foreach (var element in Content.Children)
		{
			((Entry)element).Text = "";
		}
	}
}