

using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TODOlist.Models;
using TODOlist.Service;

namespace TODOlist.ViewModel;
    [QueryProperty("Text", "Text")]

    public partial class EditViewModel : Popup
{
    public List<ToDoModel> ToDolist { get; set; }

    public ICommand CloseCommand { get; set; }
    public ObservableCollection<string> Items { get; set; }
    //[ObservableProperty]
    private ToDoModel _todo;
    public ToDoModel Todo
    {
        get { return _todo; }
        set
        {
            if (_todo != value)
            {
                _todo = value;
                OnPropertyChanged(nameof(Todo));
            }
        }
    }

    public ToDoModel ToSaveOnDb { get; set; }

    public string Text { get; set; }

    //private readonly DbConnection _dbConnection;


    public EditViewModel()
    {
        Items = new ObservableCollection<string>();

    }
    [RelayCommand]

    void Delete(string s)
    {
        Console.WriteLine('3');

        char[] b = new char[s.Length];
        StringReader sr = new StringReader(s);
        sr.Read(b, 0, 13);
        Console.WriteLine(b);
        if (Items.Contains(s))
        {
            Items.Remove(s);
        }
        Close();
    }
    [RelayCommand]
    void Add()
    {
        /*if (string.IsNullOrWhiteSpace(Text))
            return;
        Items.Add(Text);
        Text = string.Empty;*/
        CloseCommand = new Command(() =>
        {
            // Logic to close the pop-up screen
        });
    }


}


 
