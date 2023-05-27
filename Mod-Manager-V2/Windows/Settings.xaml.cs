using System.Windows;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;

namespace Mod_Manager_V2.Windows
{
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            SettingsVariables.SettingsStatus = false;
            Close();
        }
    }
}
