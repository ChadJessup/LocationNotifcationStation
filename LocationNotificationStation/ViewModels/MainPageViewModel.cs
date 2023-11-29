using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationNotificationStation.Models;
using System.Collections.ObjectModel;

namespace LocationNotificationStation.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly INotificationLocationStationRepository repository;

    [ObservableProperty]
    private ObservableCollection<LocationNotification> items = [];

    public MainPageViewModel(INotificationLocationStationRepository repository)
    {
        this.repository = repository;
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
}
