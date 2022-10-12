using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class FacilityDoctorsPage : ContentPage, PageBase
{
	FacilityDoctorsViewModel _facilityDoctorsViewModel;
    public FacilityDoctorsPage()
	{
		InitializeComponent();

		_facilityDoctorsViewModel = new FacilityDoctorsViewModel("Zarz¹dzaj lekarzami", this,
			InstanceManager.FacilitiesViewModel.CurrentFacility);

		BindingContext = _facilityDoctorsViewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		_facilityDoctorsViewModel.Load();
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