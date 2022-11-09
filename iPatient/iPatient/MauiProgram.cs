using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using ZXing.Net.Maui;

namespace iPatient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseBarcodeReader()
			.UseMauiCommunityToolkitCore()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("ReemKufilnk-Regular.ttf", "ReemKufRegular");
			});

		return builder.Build();
	}
}
