using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODOlist.Models;
using TODOlist.Service;
using TODOlist.View;

namespace TODOlist.ViewModel;

 public partial class MainPageViewModel : BaseViewModel , IQueryAttributable
{
    [ObservableProperty]
    List<ToDoModel> todolist;

    [ObservableProperty]
    ToDoModel todo;

    [ObservableProperty]
    ToDoModel toSaveOnDB;

    [ObservableProperty]
    ToDoModel toDeleteOnDB;

    private readonly DbConection _dbConnection;
    public MainPageViewModel(DbConection dbConnection)
    {
        _dbConnection = dbConnection;
        //Title = "ToDoList Page";
        Todolist = new List<ToDoModel>();
        toSaveOnDB = new ToDoModel();
        todo = new ToDoModel();
        GetInitialData();
    }
    [RelayCommand]
    private async     Task
GetInitialData()
    {
        var items = await _dbConnection.GetItemsAsync();

        // Menține starea IsCompleted
        foreach (var item in items)
        {
            var existingItem = Todolist.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem != null)
            {
                item.IsCompleted = existingItem.IsCompleted;
            }
        }

        Todolist = items;
    }


    [RelayCommand]
    async Task GoToEditPage(ToDoModel toDo)
    {

        Todo.Id = -2;
        var navigationParameter = new Dictionary<string, object>
        {
            { "Todo", toDo }
        };

        toDo = null;
        await Shell.Current.GoToAsync(nameof(Edit),navigationParameter);
    }

    [RelayCommand]
    private async void GoToAddPage()
    {
        
        var navigationParameter = new Dictionary<string, object>
        {
            { "Todo", Todo }
        };
        Todo.Id = -1;
        await Shell.Current.GoToAsync($"{nameof(Add)}", navigationParameter);
    }

    [RelayCommand]
    private async void SaveOnDb()
    {
        if (toSaveOnDB != null)
        {
            await _dbConnection.SaveItemAsync(toSaveOnDB);
            Todolist = await _dbConnection.GetItemsAsync(); // Reîncarcă lista, dar păstrează IsCompleted
        }
    }

    [RelayCommand]
    public async Task DeleteOnDb(ToDoModel todo)
    {
        if (todo == null) return;

        Todolist.Remove(todo);
        await _dbConnection.DeleteItemAsync(todo);

        // Refresh the list after deletion
        Todolist = await _dbConnection.GetItemsAsync();
    }


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (Todo.Id == -2 && query.ContainsKey("IdUser"))
        {
            if (query["IdUser"] == null)
                return;

            // Ensure safe type conversion
            if (query["IdUser"] is int id || (query["IdUser"] is string idStr && int.TryParse(idStr, out id)))
            {
                var todoItem = Todolist.FirstOrDefault(x => x.Id == id);

                if (todoItem != null)
                {
                    Todolist.Remove(todoItem); // Explicitly remove it from the list
                    if (todoItem.Val == 0)
                    {
                        deleteOnDbCommand.Execute(todoItem);
                    }
                }
            }
        }

        if (Todo.Id == -1 && query.ContainsKey("NameUser"))
        {
            Console.WriteLine(Todo.Name);

            if (query["NameUser"] is ToDoModel element)
            {
                ToSaveOnDB = element;
                Todolist.Add(element);
            }
            else
            {
                return; // If it's not a ToDoModel, exit early.
            }
        }

        // Make sure to properly await the async method
        Task.Run(async () => await GetInitialData());
    }


}






