using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class FacilitiesPage : ContentPage, PageBase
{
	private FacilitiesViewModel _facilitiesViewModel;
	public FacilitiesPage()
	{
		InitializeComponent();

		_facilitiesViewModel = new FacilitiesViewModel("Zarządzaj placówkami", this);

		InstanceManager.FacilitiesViewModel = _facilitiesViewModel;
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