using Mod_Manager.ViewModels;
using Wpf.Ui.Controls;

namespace Mod_Manager.Views;

public partial class ModPage : INavigableView<ModPageViewModel>
{
    public ModPageViewModel ViewModel { get; }
    
    public ModPage(ModPageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
    }
}