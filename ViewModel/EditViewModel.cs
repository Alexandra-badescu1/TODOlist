
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TODOlist.Messages;
using TODOlist.Models;
using TODOlist.Service;

namespace TODOlist.ViewModel;

public partial class EditViewModel : BaseViewModel, IQueryAttributable
{

    [ObservableProperty]
    ToDoModel text;

    [ObservableProperty]
    List<ToDoModel> todolist;

    [ObservableProperty]
    ToDoModel toSaveOnDB;

    private readonly DbConection connection;

    public EditViewModel(DbConection dbConnection)
    {
        connection = dbConnection;
        Todolist = new List<ToDoModel>();
        toSaveOnDB = new ToDoModel();
        GetInitalDataCommand.Execute(null);
    }

    [ObservableProperty]
    ObservableCollection<string> items;


    [RelayCommand]
    private async void GetInitalData()
    {
        Todolist = await connection.GetItemsAsync();
    }
    
    
    [RelayCommand]
    async Task SaveOnDb()
   {
        if (ToSaveOnDB.Name == null)
            return;
        ToSaveOnDB.Val = 1;
        await connection.UpdateItemAsync(ToSaveOnDB);
        BackCommand.Execute(null);
    }

    [RelayCommand]
    async Task Delete()
    {
        WeakReferenceMessenger.Default.Send(new DeleteItemMessages(ToSaveOnDB));
        Todolist.Remove(ToSaveOnDB);
        ToSaveOnDB.Val = 0;
        await Shell.Current.GoToAsync("..");
        BackCommand.Execute(null);
    }

    [RelayCommand]

    private async Task Back()
    {
        //var query = new Dictionary<string, object>();
        if (ToSaveOnDB.Val == 1 || ToSaveOnDB.Val == 0)
        {
            var parameters = new Dictionary<string, object>()
             {
                 {"IdUser",ToSaveOnDB.Id }
             };
            await Shell.Current.GoToAsync("..", parameters);
        }
        else 
        {
            var parameters = new Dictionary<string, object>()
             {
                 {"IdUser",null }
             };
            await Shell.Current.GoToAsync("..", parameters);
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ToSaveOnDB = query["Todo"] as ToDoModel;
        ToSaveOnDB.Val = -1;
    }
}



