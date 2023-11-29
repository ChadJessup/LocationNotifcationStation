using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationNotificationStation.Models;

namespace LocationNotificationStation.ViewModels;

[QueryProperty("Notification", "Notification")]
public partial class DetailPageViewModel : ObservableObject
{
    public DetailPageViewModel()
    {

    }

    [ObservableProperty]
    private LocationNotification notification;

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
