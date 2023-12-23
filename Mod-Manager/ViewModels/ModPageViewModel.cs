using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

namespace Mod_Manager.ViewModels;

public partial class ModPageViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private bool _isInitialized;

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
        IsInitialized = true;
    }
}