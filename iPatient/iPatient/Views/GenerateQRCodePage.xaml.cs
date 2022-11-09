using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class GenerateQRCodePage : ContentPage, PageBase
{
	private GenerateQRCodeViewModel _generateQRCodeViewModel;
	public GenerateQRCodePage()
	{
		InitializeComponent();

		_generateQRCodeViewModel = new GenerateQRCodeViewModel("Kod QR dla placówki", this, InstanceManager.FacilitiesViewModel.CurrentFacility.Id);

		BindingContext = _generateQRCodeViewModel;
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