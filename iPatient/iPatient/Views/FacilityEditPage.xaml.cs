using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.Model;
using iPatient.ViewModels;

namespace iPatient.Views;
public partial class FacilityEditView : ContentPage, PageBase
{
	private FacilityEditViewModel _facilityEditViewModel;


    public FacilityEditView()
    {
        InitializeComponent();

        _facilityEditViewModel = new FacilityEditViewModel("Edytuj dane placówki", this, 
			InstanceManager.FacilitiesViewModel.CurrentFacility);

		ManageDoctorsButton.IsVisible = InstanceManager.FacilitiesViewModel.CurrentFacility != null;

        BindingContext = _facilityEditViewModel;
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