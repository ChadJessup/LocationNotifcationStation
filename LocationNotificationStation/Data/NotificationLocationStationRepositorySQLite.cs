using LocationNotificationStation.Models;
using SQLite;

namespace LocationNotificationStation.Data;

public class NotificationLocationStationRepositorySQLite : INotificationLocationStationRepository
{
    private SQLiteAsyncConnection? Database;

    public NotificationLocationStationRepositorySQLite()
    {
    }

    private async Task Init()
    {
        if (Database is not null)
        {
            return;
        }

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<LocationNotification>();
    }

    public async Task<LocationNotification> GetItemAsync(int id)
    {
        await Init();

        return await Database!
            .Table<LocationNotification>()
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<LocationNotification>> GetItemsAsync()
    {
        await Init();

        return await Database!
            .Table<LocationNotification>()
            .ToListAsync();
    }

    public async Task<int> SaveItemAsync(LocationNotification item)
    {
        await Init();

        if (item.Id != 0)
        {
            return await Database!.UpdateAsync(item);
        }
        else
        {
            return await Database!.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(LocationNotification item)
    {
        await Init();

        return await Database!.DeleteAsync(item);
    }
}
