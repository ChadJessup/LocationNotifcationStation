using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationNotificationStation;

public class NotificationLocationStationDatabase
{
    SQLiteAsyncConnection Database;

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
        return await Database.Table<LocationNotification>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(LocationNotification item)
    {
        await Init();
        if (item.ID != 0)
        {
            return await Database.UpdateAsync(item);
        }
        else
        {
            return await Database.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(LocationNotification item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}
