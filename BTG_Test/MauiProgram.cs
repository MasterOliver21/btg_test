using BTG_Test.ViewModels;
using BTG_Test.Views;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace BTG_Test;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<GraphicViewModel>();
        builder.Services.AddSingleton<GraphicPage>();
        builder.Services.AddSingleton<MainPage>();



        return builder.Build();
	}
}
