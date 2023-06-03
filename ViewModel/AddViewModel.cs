using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TODOlist.Messages;
using TODOlist.Models;
using TODOlist.Service;

namespace TODOlist.ViewModel;

[QueryProperty(nameof(text),nameof(text))]
public partial class AddViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    ToDoModel text;

    [ObservableProperty]
    List<ToDoModel> todolist;

    [ObservableProperty]
    ToDoModel todolist_val;

    [ObservableProperty]
    ToDoModel toSaveOnDB;

    private readonly DbConection connection;

    public AddViewModel(DbConection dbConnection)
    {
        connection = dbConnection;
        todolist = new List<ToDoModel>();
        toSaveOnDB = new ToDoModel();
        GetInitalDataCommand.Execute(null);
        //Todo.Ok = 0;
    }

    [ObservableProperty]
    ObservableCollection<string> items;


    [RelayCommand]
    private async void GetInitalData()
    {
        Todolist = await connection.GetItemsAsync();
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Todolist_val = query["Todo"] as ToDoModel;
        Todolist_val.Val = -1;
    }

    [RelayCommand]
    private async Task SaveOnDb()
    {
        if (ToSaveOnDB.Name == null)
            return;
        await connection.SaveItemAsync(ToSaveOnDB);
        Todolist_val.Val = -1;
        BackCommand.Execute(null);
    }

    [RelayCommand]
    async Task Delete()
    {
        WeakReferenceMessenger.Default.Send(new DeleteItemMessages(text));
        await Shell.Current.GoToAsync("..");

    }

    [RelayCommand]

    private async Task Back()
    {
        //var query = new Dictionary<string, object>();
        if (Todolist_val.Val == -1)
        {
            Console.WriteLine("3");
            var parameters = new Dictionary<string, object>()
             {
                 {"NameUser",ToSaveOnDB }
                 //{"NameUser2",Todo.Id}
             };
            await Shell.Current.GoToAsync("..", parameters);
        }
        else
        {
            var parameters = new Dictionary<string, object>()
             {
                 {"NameUser",null }
             };
            await Shell.Current.GoToAsync("..", parameters);
        }
    }
}
