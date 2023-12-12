using System.Windows.Input;
using Mod_Manager.ViewModels;
using Wpf.Ui.Controls;

namespace Mod_Manager.Views;

public partial class ModRepositoryPage : INavigableView<ModRepositoryPageViewModel>
{
    public ModRepositoryPageViewModel ViewModel { get; }
    
    public ModRepositoryPage(ModRepositoryPageViewModel viewModel)
    {
        ViewModel = viewModel;
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
}