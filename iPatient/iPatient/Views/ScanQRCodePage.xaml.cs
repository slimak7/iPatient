using iPatient.ViewModels;

namespace iPatient.Views;

public partial class ScanQRCodePage : ContentPage
{
	private ScanQRCodeViewModel _scanQRCodeViewModel;
	public ScanQRCodePage()
	{
		InitializeComponent();

		_scanQRCodeViewModel = new ScanQRCodeViewModel("Odbierz wizytê kodem QR", this);

		BindingContext = _scanQRCodeViewModel;
	}

	private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
		_scanQRCodeViewModel.QRCodeReaded(e.Results[0].Value);
	}
}