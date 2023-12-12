using Mod_Manager.ViewModels;
using Wpf.Ui.Controls;

namespace Mod_Manager.Views;

public partial class HomePage : INavigableView<HomePageViewModel>
{
    public HomePageViewModel ViewModel { get; }
    
    public HomePage(HomePageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
    }
}