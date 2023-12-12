using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Mod_Manager.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Mod_Manager.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isInitialized;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<object> _mainItems = new();

    [ObservableProperty]
    private ObservableCollection<object> _footerItems = new();

    public MainWindowViewModel(INavigationService navigationService)
    {
        if (IsInitialized)
        {
            return;
        }

        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        ApplicationTitle = "Forza Mods - Mod Manager";
        
        MainItems = new ObservableCollection<object>
        {
            new NavigationViewItem
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(HomePage)
            },
            new NavigationViewItem
            {
                Content = "Mod Repository",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Library24 },
                TargetPageType = typeof(ModRepositoryPage)
            },
            new NavigationViewItem
            {
                Content = "Mod Page Example",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Library24 },
                TargetPageType = typeof(ModPage)
            }
        };

        FooterItems = new ObservableCollection<object>
        {
            new NavigationViewItem
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(SettingsPage)
            }
        };

        IsInitialized = true;
    }
}