using CommunityToolkit.Maui.Views;
using iPatient.Managers;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class DoctorVisitBookingPage : ContentPage, PageBase
{
	private DoctorVisitBookingViewModel _doctorVisitBookingViewModel;
	public DoctorVisitBookingPage()
	{
		InitializeComponent();

		_doctorVisitBookingViewModel = new DoctorVisitBookingViewModel("Szczeg�y", this, InstanceManager.FindDoctorViewModel.CurrentDoctor);

		BindingContext = _doctorVisitBookingViewModel;
	}

	public void ShowPopupPage(WaitingPopupPage popupPage)
	{
		this.ShowPopup(popupPage);
	}

	public void ShowPopupPage(InfoPopupPage infoPopupPage)
	{
		this.ShowPopup(infoPopupPage);
	}

	public void ShowPopupPage(ConfirmPopupPage confirmPopupPage)
	{
		this.ShowPopup(confirmPopupPage);
	}
}