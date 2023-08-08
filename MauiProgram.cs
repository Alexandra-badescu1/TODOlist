
/* Unmerged change from project 'TODOlist (net7.0-windows10.0.19041.0)'
Before:
using Microsoft.Extensions.DependencyInjection;
After:
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
*/

/* Unmerged change from project 'TODOlist (net7.0-android)'
Before:
using Microsoft.Extensions.DependencyInjection;
After:
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
*/

/* Unmerged change from project 'TODOlist (net7.0-ios)'
Before:
using Microsoft.Extensions.DependencyInjection;
After:
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
*/
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using
/* Unmerged change from project 'TODOlist (net7.0-windows10.0.19041.0)'
Before:
using TODOlist.View;
using TODOlist.ViewModel;
using ToDolist.Services;
using TODOlist.Service;
After:
using ToDolist.Services;
using TODOlist.Service;
using TODOlist.View;
using TODOlist.ViewModel;
*/

/* Unmerged change from project 'TODOlist (net7.0-android)'
Before:
using TODOlist.View;
using TODOlist.ViewModel;
using ToDolist.Services;
using TODOlist.Service;
After:
using ToDolist.Services;
using TODOlist.Service;
using TODOlist.View;
using TODOlist.ViewModel;
*/

/* Unmerged change from project 'TODOlist (net7.0-ios)'
Before:
using TODOlist.View;
using TODOlist.ViewModel;
using ToDolist.Services;
using TODOlist.Service;
After:
using ToDolist.Services;
using TODOlist.Service;
using TODOlist.View;
using TODOlist.ViewModel;
*/
TODOlist.Service;

namespace TODOlist;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
        builder.Services.AddSingleton<View.Add>();
        builder.Services.AddSingleton<ViewModel.AddViewModel>();
        builder.Services.AddSingleton<View.Edit>();
        builder.Services.AddSingleton<ViewModel.EditViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ViewModel.MainPageViewModel>();
        builder.Services.AddScoped<DbConection>();
#endif

        return builder.Build();
    }
}
