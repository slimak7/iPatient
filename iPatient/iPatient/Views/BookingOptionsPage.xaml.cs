using CommunityToolkit.Maui.Views;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class BookingOptionsPage : ContentPage, PageBase
{
	private BookingOptionsViewModel _bookingOptionsViewModel;
	public BookingOptionsPage()
	{
		InitializeComponent();

		_bookingOptionsViewModel = new BookingOptionsViewModel("Zarezeruj wizytê", this);

		BindingContext = _bookingOptionsViewModel;
	}

	public void ShowPopupPage(WaitingPopupPage popupPage)
	{
		this.ShowPopup(popupPage);
	}

	public void ShowPopupPage(InfoPopupPage infoPopupPage)
	{
		this.ShowPopup(infoPopupPage);
	}
}