using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LocationNotificationStation.Data;
using LocationNotificationStation.Models;
using System.Collections.ObjectModel;

namespace LocationNotificationStation.ViewModels;

public partial class MainPageViewModel : ObservableRecipient
{
    private readonly INotificationLocationStationRepository repository;
    private readonly IMessenger messenger;

    [ObservableProperty]
    private ObservableCollection<LocationNotification> items = [];

    [ObservableProperty]
    private bool isRefreshing = false;

    [ObservableProperty]
    private string locationDebug = string.Empty;

    [ObservableProperty]
    private Location lastLocation;

    public MainPageViewModel(INotificationLocationStationRepository repository, IMessenger messenger, IDeviceInfo di)
    {
        this.repository = repository;
        this.messenger = messenger;

        this.messenger.Register(this, (MainPageViewModel r, StartServiceMessage message) =>
        {
            LocationDebug = "Received StartService message";
        });

        this.messenger.Register(this, (MainPageViewModel r, LocationMessage message) =>
        {
            LastLocation = message.Value;
            LocationDebug = $"{DateTime.Now.ToShortDateString()} {message.Value.Latitude} {message.Value.Longitude}";
        });

        if (Preferences.Get("LocationServiceRunning", false))
        {
            StartService();
        }
    }

    private void StartService()
    {
        var startServiceMessage = new StartServiceMessage();
        this.messenger.Send(startServiceMessage);

        Preferences.Set("LocationServiceRunning", true);

    }

    [RelayCommand]
    private async Task GetLocationsAsync()
    {
        var results = await this.repository.GetItemsAsync();

        if (Items.Any())
        {
            Items.Clear();
        }

        foreach (var location in results)
        {
            Items.Add(location);
        }

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task GoToDetailsAsync(LocationNotification ln)
    {
        if (ln is null)
        {
            return;
        }

        await Shell.Current.GoToAsync(
            nameof(DetailPage),
            true,
            new Dictionary<string, object>
            {
                { "Notification", ln},
            });
    }

    [RelayCommand]
    private async Task NewLocationNotification()
    {

    }

    public async Task CheckPermissions()
    {
        var locationPerms = await CheckLocationPermissions();
        var result = await RequestLocationPermission(locationPerms);

        if (!Preferences.Get("LocationServiceRunning", false) && result == PermissionStatus.Granted)
        {
            StartService();
        }
    }

    public async Task<PermissionStatus> RequestLocationPermission(PermissionStatus locationPerms)
    {
        try
        {
            if (locationPerms == PermissionStatus.Granted)
            {
                return locationPerms;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationAlways>())
            {
                await Shell.Current.DisplayAlert("Needs Permissions", "BECAUSE!!!", "OK");
            }

            locationPerms = await Permissions.RequestAsync<Permissions.LocationAlways>();
        }
        catch (Exception ex)
        {
            //Error that permissions request must be on main thread **
            Console.WriteLine(ex.Message);
        }

        return locationPerms;
    }

    private static async Task<PermissionStatus> CheckLocationPermissions()
    {
        var locationPermission = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
        return locationPermission;
    }
}
