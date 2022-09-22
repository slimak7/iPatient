using CommunityToolkit.Maui.Views;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class StartPage : ContentPage
{
	private StartPageViewModel _startPageViewModel;

	public StartPage()
	{
		InitializeComponent();

		_startPageViewModel = new StartPageViewModel(ShowPopupPage, "Welcome to iPatient!");
		BindingContext = _startPageViewModel;
	}

	private void ShowPopupPage(WaitingPopupPage popupPage)
	{
		this.ShowPopup(popupPage);
	}
}