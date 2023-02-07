using CommunityToolkit.Maui.Views;
using iPatient.Animations;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class WaitingPopupPage : Popup
{
	private WaitingPopupViewModel _popupViewModel;

	public WaitingPopupPage(Func<Task<bool>> actionToPerform, Action actionIfSuccess, Action actionIfFailed, string description)
	{
		InitializeComponent();

		InfoLabel.Text = description;

		RainbowBackgroundColorAnimation animation = new RainbowBackgroundColorAnimation();
		animation.Animate(InfoLabel);
		
		_popupViewModel = new WaitingPopupViewModel(actionToPerform, actionIfSuccess, actionIfFailed, this);
	}

}