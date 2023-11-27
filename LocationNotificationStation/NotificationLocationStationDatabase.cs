using SQLite;

namespace LocationNotificationStation;

public class NotificationLocationStationDatabase
{
    private SQLiteAsyncConnection? Database;

    public NotificationLocationStationDatabase()
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

    public async Task<int> SaveItemAsync(LocationNotification item)
    {
        await Init();

        Location? location = await Geolocation.Default.GetLastKnownLocationAsync();

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
