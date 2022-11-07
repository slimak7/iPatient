using CommunityToolkit.Maui.Views;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class ConfirmPopupPage : Popup
{
	private ConfirmPopupViewModel _confirmPopupViewModel;
	public ConfirmPopupPage(string message, Action actionToPerform)
	{
		InitializeComponent();

		_confirmPopupViewModel = new ConfirmPopupViewModel(message, this, actionToPerform);

		BindingContext = _confirmPopupViewModel;
	}
}