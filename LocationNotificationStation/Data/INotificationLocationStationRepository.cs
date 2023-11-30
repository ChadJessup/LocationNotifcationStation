using LocationNotificationStation.Models;

namespace LocationNotificationStation.Data;

public interface INotificationLocationStationRepository
{
    Task<LocationNotification> GetItemAsync(int id);
    Task<List<LocationNotification>> GetItemsAsync();
    Task<int> SaveItemAsync(LocationNotification item);
    Task<int> DeleteItemAsync(LocationNotification item);
}