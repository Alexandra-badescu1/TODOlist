using TODOlist.ViewModel;

namespace TODOlist.View;

public partial class Add : ContentPage
{
    public Add(AddViewModel add)
    {
        InitializeComponent();
        BindingContext = add;
    }
}