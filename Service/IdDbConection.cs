using TODOlist.Models;

namespace ToDolist.Services
{
    public interface IDbConnection
    {
        Task<int> DeleteItemAsync(ToDoModel item);
        Task<ToDoModel> GetItemAsync(int id);
        Task<List<ToDoModel>> GetItemsAsync();
        Task<int> SaveItemAsync(ToDoModel item);
        Task<int> UpdateItemAsync(ToDoModel item);
    }
}