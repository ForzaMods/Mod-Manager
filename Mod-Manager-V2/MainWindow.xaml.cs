using System;
using System.Windows;
using IniParser.Model;
using IniParser;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using Mod_Manager_V2.Windows;
using System.Windows.Input;

namespace Mod_Manager_V2
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            SettingsFile.CreateSettingsFile();

            #region Path Checking shit
            string ShittySettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(ShittySettingsFile);
            if (!bool.Parse(Settings["Settings"]["Usermode"])) { CheckForPath.CheckIfFolderExists(); } else { CheckForAdmin.FirstLaunch(); }
            #endregion

            SettingsFile.CheckForDiscordRPC();
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

        private void DraggingFunctionality(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
