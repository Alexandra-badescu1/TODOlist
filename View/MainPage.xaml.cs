using CommunityToolkit.Maui.Views;
using TODOlist.View;
using TODOlist.ViewModel;

namespace TODOlist;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel mainPage)
    {
        InitializeComponent();
        BindingContext = mainPage;
    }
}

