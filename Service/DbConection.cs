using Microsoft.VisualBasic.FileIO;
using SQLite;
using TODOlist.Models;


namespace TODOlist.Service;

public class DbConection
{
    SQLiteAsyncConnection connection;

    public const SQLite.SQLiteOpenFlags flags =

    SQLite.SQLiteOpenFlags.ReadWrite |

    SQLite.SQLiteOpenFlags.Create |

    SQLite.SQLiteOpenFlags.SharedCache;
    public DbConection()
    {
    }

    async Task Init()
    {
        if (connection is not null)
            return;

        var databasePath = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "ToDoList.db");

        try
        {
            connection = new SQLiteAsyncConnection(databasePath, flags);
        }

        catch (Exception ex)
        {

        }

        await connection.CreateTableAsync<ToDoModel>();

    }

    public async Task<List<ToDoModel>> GetItemsAsync()
    {
        await Init();
        return await connection.Table<ToDoModel>().ToListAsync();
    }

    public async Task<ToDoModel> GetItemAsync(int id)
    {
        await Init();
        return await connection.Table<ToDoModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(ToDoModel item)
    {
        await Init();
        return await connection.InsertAsync(item);
    }

    public async Task<int> UpdateItemAsync(ToDoModel item)
    {
        await Init();
        return await connection.UpdateAsync(item);
    }

    public async Task<int> DeleteItemAsync(ToDoModel item)
    {
        await Init();
        return await connection.DeleteAsync(item);
    }

}