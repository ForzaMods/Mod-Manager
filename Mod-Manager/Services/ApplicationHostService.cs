using System.Windows;
using Microsoft.Extensions.Hosting;
using Mod_Manager.Views;
using Wpf.Ui;

namespace Mod_Manager.Services;

/// <summary>
/// Managed host of the application.
/// </summary>
public class ApplicationHostService(IServiceProvider serviceProvider) : IHostedService
{
    private INavigationWindow? _navigationWindow;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return HandleActivationAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task HandleActivationAsync()
    {
        await Task.CompletedTask;

        if (!Application.Current.Windows.OfType<MainWindow>().Any())
        {
            _navigationWindow = (serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            _navigationWindow.ShowWindow();
            _navigationWindow.Navigate(typeof(HomePage));
        }

        await Task.CompletedTask;
    }
}
