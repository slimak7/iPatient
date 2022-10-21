using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class FindDoctorPage : ContentPage, PageBase
{
	private FindDoctorViewModel _findDoctorViewModel;
	public FindDoctorPage()
	{
		InitializeComponent();

		_findDoctorViewModel = new FindDoctorViewModel("Znajdü lekarza dla siebie", this, InstanceManager.FacilitiesViewModel?.CurrentFacility ?? null, InstanceManager.StartPageViewModel.City);

		BindingContext = _findDoctorViewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		_findDoctorViewModel.Load();
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