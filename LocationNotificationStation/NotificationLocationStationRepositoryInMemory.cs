using LocationNotificationStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationNotificationStation;

public class NotificationLocationStationRepositoryInMemory : INotificationLocationStationRepository
{
    private List<LocationNotification> items = [];

    public NotificationLocationStationRepositoryInMemory()
    {
        items.Add(new LocationNotification
        {
            CreatedAt = DateTime.Now,
            Description = "Don't get the rum and coke at JJ's!",
            Id = 1,
            IsEnabled = true,
            Latitude = 0.12093,
            Longitude = 123.12390823,
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
