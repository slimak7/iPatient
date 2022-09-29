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

	public void SetUserInfo(User user)
	{
		FirstNameLabel.Text = user.FirstName;
		LastNameLabel.Text = user.LastName;
		EmailLabel.Text = user.Email;
		PeselLabel.Text = user.PESEL;
		PhoneLabel.Text = user.PhoneNumber;
	}
}