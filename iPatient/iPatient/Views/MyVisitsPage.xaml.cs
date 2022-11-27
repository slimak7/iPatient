using CommunityToolkit.Maui.Views;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class MyVisitsPage : ContentPage, PageBase
{
	private MyVisitsViewModel _myVisitsViewModel;
	public MyVisitsPage()
	{
		InitializeComponent();

		_myVisitsViewModel = new MyVisitsViewModel("Moje wizyty", this);

		BindingContext = _myVisitsViewModel;

	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

        _myVisitsViewModel.Load();
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