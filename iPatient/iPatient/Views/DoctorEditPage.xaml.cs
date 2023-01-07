using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class DoctorEditPage : ContentPage, PageBase
{
	private DoctorEditViewModel _doctorEditViewModel;
	public DoctorEditPage()
	{
		InitializeComponent();

		_doctorEditViewModel = new DoctorEditViewModel("Edytuj dane lekarza", this,
			InstanceManager.FacilitiesViewModel.CurrentFacility, InstanceManager.FacilityDoctorsViewModel.CurrentDoctor,
			InstanceManager.FacilityDoctorsViewModel.Specializations);

		BindingContext = _doctorEditViewModel;

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