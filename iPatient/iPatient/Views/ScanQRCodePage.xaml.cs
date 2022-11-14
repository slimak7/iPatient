using CommunityToolkit.Maui.Views;
using iPatient.ViewModels;

namespace iPatient.Views;

public partial class ScanQRCodePage : ContentPage, PageBase
{
	private ScanQRCodeViewModel _scanQRCodeViewModel;
	public ScanQRCodePage()
	{
		InitializeComponent();

		_scanQRCodeViewModel = new ScanQRCodeViewModel("Odbierz wizytê kodem QR", this);

		BindingContext = _scanQRCodeViewModel;
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

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
		Scanner.IsDetecting = false;
		Dispatcher.Dispatch(() =>_scanQRCodeViewModel.QRCodeReaded(e.Results[0].Value));
	}
}