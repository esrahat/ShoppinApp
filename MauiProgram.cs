using Microsoft.Extensions.Logging;
using ShopNow.ViewModels;
using ShopNow.Services;

namespace ShopNow;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>(serviceProvider =>
			{
				string dbPath = Path.Combine(FileSystem.AppDataDirectory, "shopnow.db3");  // Define dbPath here
				return new App(dbPath);  // Pass the dbPath to the App constructor
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		// Registering dependencies
		builder.Services.AddTransient<CartViewModel>();  // Register CartViewModel
		builder.Services.AddTransient<Item>();  // Register Item if needed
												// Register ReviewDatabaseService
												// Register the ReviewDatabaseService for Dependency Injection
		builder.Services.AddSingleton<DatabaseService>();

		return builder.Build();
	}
}
