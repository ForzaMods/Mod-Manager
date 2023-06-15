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

        private void DiscordRPCButton_Toggled(object sender, RoutedEventArgs e)
        {
            #region Strings for settings file
            var toggleSwitch = (ToggleSwitch)sender;
            string SettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            #endregion


            if (toggleSwitch.IsOn)
            {
                #region Save Setings into a file and initialize
                if (Settings["Settings"]["Discord Rich Presence"].ToString() == "False")
                {
                    Settings["Settings"]["Discord Rich Presence"] = "True";
                    SettingsParser.WriteFile(SettingsFile, Settings);
                    DiscordRichPresence.RPCInitialize();
                }
                #endregion
            }
            else
            {
                #region Save Setings into a file and deinitalize
                if (Settings["Settings"]["Discord Rich Presence"].ToString() == "True")
                {
                    Settings["Settings"]["Discord Rich Presence"] = "False";
                    SettingsParser.WriteFile(SettingsFile, Settings);
                    DiscordRichPresence.RPCDeInitialize();
                }
                #endregion
            }
        }

        private void RevertCRS_Click(object sender, RoutedEventArgs e)
        {
            var RenderScenariosPath = MainWindow.BaseDirectory + @"\media\RenderScenarios.zip";
            if (File.Exists(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip")) { File.Copy(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip", RenderScenariosPath); }
            else if (!File.Exists(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip"))
            {
                Hide();
                ErrorReportingVariables.ErrorReportingWindow.ErrorCode.Content = "Stock not found, do you wanna download?";
                ErrorReportingVariables.ErrorReportingWindow.Install.Visibility = Visibility.Visible;
                ErrorReportingVariables.ErrorReportingWindow.Show();
                ErrorReporting.InstallInt = 2;
            }
        }

        private void TitleLabel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
