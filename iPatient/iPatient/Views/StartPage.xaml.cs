using iPatient.ViewModels;

namespace iPatient.Views;

public partial class StartPage : ContentPage
{
	private StartPageViewModel _startPageViewModel;
	public StartPage()
	{
		InitializeComponent();

		_startPageViewModel = new StartPageViewModel();
		BindingContext = _startPageViewModel;
	}
}