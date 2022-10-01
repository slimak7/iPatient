using iPatient.Model;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class AccountInfoView : ContentView
{
	public AccountInfoView()
	{
        InitializeComponent();
    }
	public AccountInfoView(StartPageViewModel startPageViewModel)
	{
		InitializeComponent();

		BindingContext = startPageViewModel;
	}

	public void SetUserInfo(User user, Address address)
	{
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
}