using CommunityToolkit.Maui.Views;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class InfoPopupPage : Popup
{
	public InfoPopupPage(string info)
	{
		InitializeComponent();

		InfoLabel.Text = info;

	}

	
}