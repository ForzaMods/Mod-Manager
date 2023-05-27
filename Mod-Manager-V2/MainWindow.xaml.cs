using System.Windows;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using Mod_Manager_V2.Windows;

namespace Mod_Manager_V2
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            SettingsFile.CreateSettingsFile();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Settings_Button(object sender, RoutedEventArgs e)
        {
            if (!SettingsVariables.SettingsStatus) { SettingsVariables.SettingsWindow.Show(); SettingsVariables.SettingsStatus = true; }
        }
        
        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow || window is ErrorReporting || window is Settings)
                {
                    window.Close();
                }
            }
        }
    }
}
