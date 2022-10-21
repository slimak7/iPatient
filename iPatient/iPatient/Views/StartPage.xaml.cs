using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.Model;
using iPatient.ViewModels;
using System.Net;

namespace iPatient.Views;

public partial class StartPage : ContentPage, PageBase
{
	private StartPageViewModel _startPageViewModel;
	private AccountInfoView _accountInfoView;
	private LoginView _loginView;
	private RegisterView _registerView;

	private const int _borderWidthClicked = 6;
	private const int _borderWidthUnclicked = 2;

	public StartPage()
	{
		InitializeComponent();

        _startPageViewModel = new StartPageViewModel("Witaj w iPatient!", this);
        BindingContext = _startPageViewModel;

		_accountInfoView = new AccountInfoView(_startPageViewModel);
		_loginView = new LoginView(_startPageViewModel);
		_registerView = new RegisterView(_startPageViewModel);

		LoginScrollView.Content = _loginView;
		RegisterScrollView.Content = _registerView;
		AccountInfoScrollView.Content = _accountInfoView;

        
        InstanceManager.StartPageViewModel = _startPageViewModel;
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

		MenuView.IsVisible = true;
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

	public void SetMenuButtons(Dictionaries.Dictionary.UserRoles.Roles role)
	{
		switch(role)
		{
			case Dictionaries.Dictionary.UserRoles.Roles.Staff:

				FacilitiesButton.IsVisible = true;
				BookVisitButton.IsVisible = false;

			break;

			case Dictionaries.Dictionary.UserRoles.Roles.Patient:

				BookVisitButton.IsVisible = true;
				FacilitiesButton.IsVisible = false;

			break;
		}
	}

	public void ExpandCollapseUserInfo()
	{
		_accountInfoView.ExpandCollapseUserInfo();
	}

	public void LogOut()
	{
		_loginView.Reset();
		_registerView.Reset();
		_accountInfoView.Reset();

		SetLoginButton(true);
		SetRegisterButton(false);

        AccountInfoScrollView.IsVisible = false;
        LogInButton.IsVisible = RegisterButton.IsVisible = ContinueButton.IsVisible
            = InfoLabel.IsVisible = LoginScrollView.IsVisible = true;


        MenuView.IsVisible = false;
		FacilitiesButton.IsVisible = false;

    }
}