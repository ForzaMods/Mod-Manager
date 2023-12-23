using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mod_Manager.Services;
using Mod_Manager.ViewModels;
using Mod_Manager.Views;
using Wpf.Ui;

namespace Mod_Manager;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static readonly IHost Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(AppContext.BaseDirectory);
        })
        .ConfigureServices((_, services) => 
        {
            services.AddHostedService<ApplicationHostService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<IContentDialogService, ContentDialogService>();
            services.AddSingleton<SharedViewModel>();

            RegisterViewModels(services);
            RegisterViews(services);
        }).Build();

    private static void RegisterViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<HomePageViewModel>();
        services.AddSingleton<ModRepositoryPageViewModel>();
        services.AddSingleton<ModPageViewModel>();
        services.AddSingleton<SettingsPageViewModel>();
    }

    private static void RegisterViews(IServiceCollection services)
    {
        services.AddSingleton<INavigationWindow, MainWindow>();
        services.AddSingleton<HomePage>();
        services.AddSingleton<ModRepositoryPage>();
        services.AddSingleton<ModPage>();
        services.AddSingleton<SettingsPage>();
    }
    
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        Host.StartAsync();
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        Host.StopAsync();
        Host.Dispose();
    }

    private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
    }
}