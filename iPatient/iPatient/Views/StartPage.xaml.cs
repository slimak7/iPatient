using CommunityToolkit.Maui.Views;
using iPatient.Model;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class StartPage : ContentPage, ViewBase
{
	private StartPageViewModel _startPageViewModel;
	private AccountInfoView _accountInfoView;

	private const int _borderWidthClicked = 6;
	private const int _borderWidthUnclicked = 2;

	public StartPage()
	{
		InitializeComponent();

		_startPageViewModel = new StartPageViewModel("Witaj w iPatient!", this);
		BindingContext = _startPageViewModel;

		_accountInfoView = new AccountInfoView(_startPageViewModel);

        LoginScrollView.Content = new LoginView(_startPageViewModel);
		RegisterScrollView.Content = new RegisterView(_startPageViewModel);
		AccountInfoScrollView.Content = _accountInfoView;


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

	public void ShowPopupPage(WaitingPopupPage popupPage)
	{
		this.ShowPopup(popupPage);
	}

	public void ShowPopupPage(InfoPopupPage infoPopupPage)
	{
		this.ShowPopup(infoPopupPage);
	}

	public void ShowUserData(User user, Address address)
	{
		AccountInfoScrollView.IsVisible = true;
		LogInButton.IsVisible = RegisterButton.IsVisible = ContinueButton.IsVisible
			= InfoLabel.IsVisible = RegisterScrollView.IsVisible = LoginScrollView.IsVisible = false;

		_accountInfoView.SetUserInfo(user, address);
	}

	public void EditUserData()
	{
		_accountInfoView.EditUserInfo();
	}

    public (User user, Address address) GetUserInfo()
	{
		return _accountInfoView.GetUserInfo();
	}

	public void UserInfoSavedNoSuccess()
	{
		_accountInfoView.UserInfoSavedNoSuccess();
	}

	public void UserInfoSavedSuccess()
	{
		_accountInfoView.UserInfoSavedSuccess();
	}

	public bool IsReadyToSave()
	{
		return _accountInfoView.isReadyToSave();
	}
}