using LocationNotificationStation.Models;

namespace LocationNotificationStation.Data;

public class NotificationLocationStationRepositoryInMemory : INotificationLocationStationRepository
{
    private readonly List<LocationNotification> items = [];

    public NotificationLocationStationRepositoryInMemory()
    {
        items.Add(new LocationNotification
        {
            CreatedAt = DateTime.Now,
            Description = "Don't get the rum and coke at JJ's!",
            Id = 1,
            IsEnabled = true,
            Latitude = 47.68202421084823,
            Longitude = -122.12339573177856,
        });
    }

    public Task<int> DeleteItemAsync(LocationNotification item)
    {
        items.Remove(item);

        return Task.FromResult(1);
    }

    public Task<LocationNotification> GetItemAsync(int id)
    {
        return Task.FromResult(items[id]);
    }

    public Task<List<LocationNotification>> GetItemsAsync()
    {
        return Task.FromResult(items);
    }

    public Task<int> SaveItemAsync(LocationNotification item)
    {
        items.Add(item);

        return Task.FromResult(1);
    }
}
