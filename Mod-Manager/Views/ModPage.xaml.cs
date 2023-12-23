using Mod_Manager.ViewModels;
using Wpf.Ui.Controls;

namespace Mod_Manager.Views;

public partial class ModPage : INavigableView<ModPageViewModel>
{
    public ModPageViewModel ViewModel { get; }

    public SharedViewModel SharedViewModel { get; }
    
    public ModPage(ModPageViewModel viewModel, SharedViewModel sharedViewModel)
    {
        ViewModel = viewModel;
        SharedViewModel = sharedViewModel;
        DataContext = this;
        
        InitializeComponent();
    }
}