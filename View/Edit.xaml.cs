using TODOlist.ViewModel;

namespace TODOlist.View;


public partial class Edit : ContentPage
{
    public Edit(EditViewModel editView)
    {
        InitializeComponent();
        BindingContext = editView;
    }  
}