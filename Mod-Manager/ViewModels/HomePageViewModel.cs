using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mod_Manager.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Mod_Manager.ViewModels;

public partial class HomePageViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private bool _isInitialized;

    [ObservableProperty] 
    private string? _versionText;

    public void OnNavigatedTo()
    {
        if (IsInitialized)
        {
            return;
        }

        InitializeViewModel();
    }

    public void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        VersionText = $"Tool Version - {Assembly.GetExecutingAssembly().GetName().Version!.ToString()}";
        IsInitialized = true;
    }

    [RelayCommand]
    private void OnPageSwitch(string parameter)
    {
        switch (parameter)
        {
            case "mod_repository":
            {
                navigationService.Navigate(typeof(ModRepositoryPage));                
                break;
            }

            case "game_presets":
            {

                throw new NotImplementedException();
            }

            case "settings":
            {
                navigationService.Navigate(typeof(SettingsPage));
                break;
            }

            default:
            {
                throw new ArgumentOutOfRangeException(parameter);
            }
        }
    }
}