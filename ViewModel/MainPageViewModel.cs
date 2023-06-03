using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Windows.Input;
using ToDolist.Services;
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
        todo = new ToDoModel();// #added - instantiat mobelul 
        GetInitalData();
    }

    [RelayCommand]
    private async void GetInitalData() => Todolist = await _dbConnection.GetItemsAsync();

    [RelayCommand]
    private async void GoToEditPopup()
    {
        Todo.Val = 0;
        var popup = new Edit(todo);
        await Shell.Current.ShowPopupAsync(popup);
        //await Shell.Current.GoToAsync($"{nameof(Edit)}");
    }

    [RelayCommand]
    private async void GoToAddPage()
    {
        
        var navigationParameter = new Dictionary<string, object>
        {
            { "Todo", Todo }
        };
        todo.Val = -1;
        await Shell.Current.GoToAsync($"{nameof(Add)}", navigationParameter);
    }

    [RelayCommand]
    private async void SaveOnDb()
    {
        await _dbConnection.SaveItemAsync(toSaveOnDB);
        Todolist = await _dbConnection.GetItemsAsync();
    }

    [RelayCommand]

    public async Task DeleteOnDb(ToDoModel todo)
    {
        Todolist.Remove(todo);
       await _dbConnection.DeleteItemAsync(todo);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if(Todo.Val == 0 && query.ContainsKey("IdUser"))
        {
            Console.WriteLine(Todo.Name);
            var id = (int)query["IdUser"];
            var todoItem = Todolist.Where(x => x.Id == id).FirstOrDefault();
            Todolist.Remove(todoItem);
            Console.WriteLine("1");
            Console.WriteLine("Before " + query);
            query = new Dictionary<string, object>();
            Console.WriteLine("After " + query);
        }
        else if (Todo.Val == -1 && query.ContainsKey("NameUser"))
        {
            Console.WriteLine(Todo.Name);

            var element = (ToDoModel)query["NameUser"];

            if (element == null)
                return;
            ToSaveOnDB = element;

            Console.WriteLine("2");
            Console.WriteLine("Before " + query);
            Todolist.Add(element);
            query = new Dictionary<string, object>();
            Console.WriteLine("After " + query);
        }

    }
}






