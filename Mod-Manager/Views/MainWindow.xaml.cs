using System.Windows;
using Mod_Manager.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Mod_Manager.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : INavigationWindow
{
    public MainWindowViewModel ViewModel { get; }

    public MainWindow(
        MainWindowViewModel viewModel,
        IServiceProvider serviceProvider,
        INavigationService navigationService,
        ISnackbarService snackBarService,
        IContentDialogService contentDialogService)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
        
        snackBarService.SetSnackbarPresenter(SnackbarPresenter);
        navigationService.SetNavigationControl(NavigationView);
        contentDialogService.SetContentPresenter(RootContentDialog);
        NavigationView.SetServiceProvider(serviceProvider);
        Loaded += (_, _) => NavigationView.IsPaneOpen = false;
    }

    #region INavigationWindow methods

    public bool Navigate(Type pageType) => NavigationView.Navigate(pageType);

    public void SetPageService(IPageService pageService) => NavigationView.SetPageService(pageService);

    public void ShowWindow() => Show();

    public void CloseWindow() => Close();

    #endregion INavigationWindow methods

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Application.Current.Shutdown();
    }

    INavigationView INavigationWindow.GetNavigation()
    {
        throw new NotImplementedException();
    }

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }
}