using CommunityToolkit.Maui.Views;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class StartPage : ContentPage
{
	private StartPageViewModel _startPageViewModel;

	private const int _borderWidthClicked = 6;
	private const int _borderWidthUnclicked = 2;

	public StartPage()
	{
		InitializeComponent();

		_startPageViewModel = new StartPageViewModel(ShowPopupPage, "Welcome to iPatient!", this);
		BindingContext = _startPageViewModel;

        LoginScrollView.Content = new LoginView(_startPageViewModel);
		RegisterScrollView.Content = new RegisterView(_startPageViewModel);
    }

	private void ShowPopupPage(WaitingPopupPage popupPage)
	{
		this.ShowPopup(popupPage);
	}

	public void SetLoginButton(bool clicked)
	{
		LogInButton.BorderWidth = (clicked) ? _borderWidthClicked : _borderWidthUnclicked;

		LoginScrollView.IsVisible = clicked;
	}

	public void SetRegisterButton(bool clicked)
	{
        RegisterButton.BorderWidth = (clicked) ? _borderWidthClicked : _borderWidthUnclicked;

		RegisterScrollView.IsVisible = clicked;
    }
}