using iPatient.Model;
using iPatient.ViewModels;
using System.Net;

namespace iPatient.Views;

public partial class AccountInfoView : ContentView
{
    private const int _borderWidthClicked = 6;
    private const int _borderWidthUnclicked = 2;

	private bool _isInEdit;
	private bool _userInfoCollapsed;

	private User _user;
	private Address _address;

    public AccountInfoView()
	{
        InitializeComponent();
    }
	public AccountInfoView(StartPageViewModel startPageViewModel)
	{
		InitializeComponent();

		_isInEdit = false;
		_userInfoCollapsed = true;

		BindingContext = startPageViewModel;

	}

	public void SetUserInfo(User user, Address address)
	{
		LogOutButton.IsVisible = true;

		_user = user;
		_address = address;

		FirstNameLabel.Text = user.FirstName;
		LastNameLabel.Text = user.LastName;
		EmailLabel.Text = user.Email;
		PeselLabel.Text = user.PESEL;
		PhoneLabel.Text = user.PhoneNumber;

		StreetLabel.Text = address.Street;
		StreetNumberLabel.Text = address.StreetNumber;
		CityLabel.Text = address.City;
		PostCodeLabel.Text = address.PostCode;

        ExpandCollapseDetails.IsVisible = user.UserRole == Dictionaries.Dictionary.UserRoles.Roles.Patient;
    }

	public void EditUserInfo()
	{
		if (!_isInEdit)
		{
			PeselLabel.IsReadOnly = PhoneLabel.IsReadOnly = 
				StreetLabel.IsReadOnly = CityLabel.IsReadOnly = StreetNumberLabel.IsReadOnly =
				PostCodeLabel.IsReadOnly = false;

			PeselLabel.BackgroundColor = PhoneLabel.BackgroundColor =
				StreetLabel.BackgroundColor = StreetNumberLabel.BackgroundColor =
				CityLabel.BackgroundColor = PostCodeLabel.BackgroundColor =
				Color.FromHex("D8B384");

			EditButton.BorderWidth = _borderWidthClicked;
			EditButton.Text = "Anuluj";

			_isInEdit = true;
		}
		else
		{
            PeselLabel.IsReadOnly = PhoneLabel.IsReadOnly =
                StreetLabel.IsReadOnly = CityLabel.IsReadOnly = StreetNumberLabel.IsReadOnly =
                PostCodeLabel.IsReadOnly = true;

			PeselLabel.BackgroundColor = PhoneLabel.BackgroundColor =
				StreetLabel.BackgroundColor = StreetNumberLabel.BackgroundColor =
				CityLabel.BackgroundColor = PostCodeLabel.BackgroundColor =
				Colors.Transparent;

            EditButton.BorderWidth = _borderWidthUnclicked;
            EditButton.Text = "Edytuj";

            SetUserInfo(_user, _address);

			_isInEdit = false;
		}
	}

	public void UserInfoSavedSuccess()
	{
		var data = GetUserInfo();

		_user = data.user;
		_address = data.address;

		EditUserInfo();
	}

	public void UserInfoSavedNoSuccess()
	{
		EditUserInfo();
	}

	public (User user, Address address) GetUserInfo()
	{
		var userInfo = new User()
		{
			Id = _user.Id,
			Email = _user.Email,
			FirstName = _user.FirstName,
			LastName = _user.LastName,
			PESEL = PeselLabel.Text,
			PhoneNumber = PhoneLabel.Text
		};

		var addresInfo = new Address()
		{
			Street = StreetLabel.Text,
			City = CityLabel.Text,
			PostCode = PostCodeLabel.Text,
			StreetNumber = StreetNumberLabel.Text
		};

		return (userInfo, addresInfo);
	}

	public bool isReadyToSave()
	{
		return _isInEdit;
	}

	public void ExpandCollapseUserInfo()
	{
		if (_isInEdit)
		{
			return;
		}

        _userInfoCollapsed = !_userInfoCollapsed;

		DetailsView.IsVisible = !_userInfoCollapsed;

		EditButton.IsVisible = SaveButton.IsVisible = !_userInfoCollapsed;

		ExpandCollapseDetails.Text = (!_userInfoCollapsed) ? "Zwiñ" : "Rozwiñ";

    }

	public void ExpandUserInfo()
	{
		if (!_userInfoCollapsed)
			return;

		ExpandCollapseUserInfo();
	}

	public void Reset()
	{
		_userInfoCollapsed = true;
		_isInEdit = false;

		DetailsView.IsVisible = false;

		FirstNameLabel.Text = "";
		LastNameLabel.Text = "";
		EmailLabel.Text = "";
		PeselLabel.Text = "";
		PhoneLabel.Text = "";

		StreetLabel.Text = "";
		StreetNumberLabel.Text = "";
		CityLabel.Text = "";
		PostCodeLabel.Text = "";

		_user = null;
		_address = null;

		EditButton.IsVisible = SaveButton.IsVisible = false;

        ExpandCollapseDetails.Text = "Rozwiñ";
    }
	
}