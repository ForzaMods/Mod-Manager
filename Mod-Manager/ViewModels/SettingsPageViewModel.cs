using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Mod_Manager.ViewModels;

public partial class SettingsPageViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private bool _isInitialized;

    [ObservableProperty]
    private ApplicationTheme _currentApplicationTheme = ApplicationTheme.Unknown;
    
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
        CurrentApplicationTheme = (ApplicationTheme)ApplicationThemeManager.GetSystemTheme();
        IsInitialized = true;
    }
}