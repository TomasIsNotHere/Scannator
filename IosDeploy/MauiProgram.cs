using Microsoft.Extensions.Logging;
using Camera.MAUI;
using ZXing.Net.Maui.Controls;

namespace IosDeploy;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCameraView()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
			
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "items.db");

        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ItemHandler>(s, dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

