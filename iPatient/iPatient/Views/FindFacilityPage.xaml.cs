using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class FindFacilityPage : ContentPage, PageBase
{
	private FindFacilityViewModel _findFacilityViewModel;
	public FindFacilityPage()
	{
		InitializeComponent();

		_findFacilityViewModel = new FindFacilityViewModel("Wybierz placówkê", this, InstanceManager.StartPageViewModel.City);

		BindingContext = _findFacilityViewModel;
	}

	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);

		InstanceManager.CurrentSelectedFacility = null;
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