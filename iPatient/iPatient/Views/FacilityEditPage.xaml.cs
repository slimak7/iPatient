using CommunityToolkit.Maui.Views;
using iPatient.Model;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class FacilityEditView : ContentPage, PageBase
{
	private FacilityEditViewModel _facilityEditViewModel;
	public FacilityEditView(Facility facility)
	{
		InitializeComponent();

		_facilityEditViewModel = new FacilityEditViewModel(null, this, facility);

        BindingContext = _facilityEditViewModel;
    }

    public FacilityEditView()
    {
        InitializeComponent();

        _facilityEditViewModel = new FacilityEditViewModel(null, this, null);

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