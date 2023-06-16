using System;
using System.Windows;
using IniParser.Model;
using IniParser;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using System.IO;
using System.Windows.Input;

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
            Hide();
        }


        private void TitleLabel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Mainsettings_Click(object sender, RoutedEventArgs e)
        {
            Mainsettingsframe.Visibility = Visibility.Visible;
            Pathsettingsframe.Visibility = Visibility.Hidden;
        }

        private void Pathsettings_Click(object sender, RoutedEventArgs e)
        {
            Mainsettingsframe.Visibility = Visibility.Hidden;
            Pathsettingsframe.Visibility = Visibility.Visible;
        }
    }
}
