using System.Windows.Controls;
using Mod_Manager.ViewModels;
using Wpf.Ui.Controls;
using static Wpf.Ui.Appearance.ApplicationTheme;
using static Wpf.Ui.Appearance.ApplicationThemeManager;

namespace Mod_Manager.Views;

public partial class SettingsPage : INavigableView<SettingsPageViewModel>
{
    public SettingsPageViewModel ViewModel { get; }
    
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
    }

    private void ColorScheme_OnChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        var selectedItem = ((ComboBoxItem)comboBox?.SelectedItem!).Content.ToString();
        
        switch (selectedItem)
        {
            case "Dark":
            {
                if (ViewModel.CurrentApplicationTheme == Dark)
                {
                    break;
                }
                
                Apply(ViewModel.CurrentApplicationTheme = Dark);
                break;
            }
            case "Light":
            {
                if (ViewModel.CurrentApplicationTheme == Light)
                {
                    break;
                }
                
                Apply(ViewModel.CurrentApplicationTheme = Light);
                break;
            }
            case "High Contrast":
            {
                if (ViewModel.CurrentApplicationTheme == HighContrast)
                {
                    break;
                }
                
                Apply(ViewModel.CurrentApplicationTheme = HighContrast);
                break;
            }
        }
    }
}