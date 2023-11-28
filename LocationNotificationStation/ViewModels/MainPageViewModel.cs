using CommunityToolkit.Mvvm.ComponentModel;

namespace LocationNotificationStation.ViewModels;

public class MainPageViewModel : ObservableObject
{
    private readonly NotificationLocationStationDatabase database;

    public MainPageViewModel(NotificationLocationStationDatabase database)
    {
        this.database = database;
    }
}
