using iPatient.Model;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class AccountInfoView : ContentView
{
    private const int _borderWidthClicked = 6;
    private const int _borderWidthUnclicked = 2;

	private bool _isInEdit;

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

		BindingContext = startPageViewModel;
	}

	public void SetUserInfo(User user, Address address)
	{
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
	}

	public void EditUserInfo()
	{
		if (!_isInEdit)
		{
			PeselLabel.IsReadOnly = PhoneLabel.IsReadOnly = 
				StreetLabel.IsReadOnly = CityLabel.IsReadOnly = 
				PostCodeLabel.IsReadOnly = false;

			PeselLabel.BackgroundColor = PhoneLabel.BackgroundColor =
				StreetLabel.BackgroundColor = StreetNumberLabel.BackgroundColor =
				CityLabel.BackgroundColor = PostCodeLabel.BackgroundColor =
				Color.FromHex("cbf3f0");

			EditButton.BorderWidth = _borderWidthClicked;
			EditButton.Text = "Anuluj";

			_isInEdit = true;
		}
		else
		{
            PeselLabel.IsReadOnly = PhoneLabel.IsReadOnly =
                StreetLabel.IsReadOnly = CityLabel.IsReadOnly =
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
			StreetNumber = StreetLabel.Text
		};

		return (userInfo, addresInfo);
	}

	public bool isReadyToSave()
	{
		return _isInEdit;
	}
}