using LocationNotificationStation.ViewModels;
using Microsoft.Extensions.Logging;

namespace LocationNotificationStation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<DetailPageViewModel>();

        //        builder.Services.AddSingleton<INotificationLocationStationRepository, NotificationLocationStationRepositorySQLite>();
        builder.Services.AddSingleton<INotificationLocationStationRepository, NotificationLocationStationRepositoryInMemory>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
