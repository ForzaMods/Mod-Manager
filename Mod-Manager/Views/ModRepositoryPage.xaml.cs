using System.Windows;
using System.Windows.Input;
using Mod_Manager.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Mod_Manager.Views;

public partial class ModRepositoryPage : INavigableView<ModRepositoryPageViewModel>
{
    private readonly INavigationService _navigationService;
    public ModRepositoryPageViewModel ViewModel { get; }

    private readonly SharedViewModel _sharedViewModel;
    
    public ModRepositoryPage(ModRepositoryPageViewModel viewModel,
        SharedViewModel sharedViewModel,
        INavigationService navigationService)
    {
        _navigationService = navigationService;
        ViewModel = viewModel;
        _sharedViewModel = sharedViewModel;
        DataContext = this;

        InitializeComponent();
    }

    private void AutoSuggestBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }
        
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(((Button)sender).Content.ToString(), out var result))
        {
            return;
        }
        
        _sharedViewModel.Mod = ViewModel.DataMods[result];
        _navigationService.Navigate(typeof(ModPage));
    }
}