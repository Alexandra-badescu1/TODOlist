using TODOlist.View;

namespace TODOlist;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Add), typeof(Add));
        Routing.RegisterRoute(nameof(Edit), typeof(Edit));
    }
}
