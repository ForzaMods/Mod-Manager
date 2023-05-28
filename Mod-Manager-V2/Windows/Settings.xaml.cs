using System;
using System.Windows;
using IniParser.Model;
using IniParser;
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
            Hide();
        }

        private void DiscordRPCButton_Toggled(object sender, RoutedEventArgs e)
        {
            #region Strings for settings file
            string SettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            #endregion

            var toggleSwitch = (ToggleSwitch)sender;
            if (toggleSwitch.IsOn)
            {
                #region Save Setings into a file and initialize
                Settings["Settings"]["Discord Rich Presence"] = "True";
                SettingsParser.WriteFile(SettingsFile, Settings);
                DiscordRichPresence.RPCInitialize();
                #endregion
            }
            else
            {
                #region Save Setings into a file and deinitalize
                Settings["Settings"]["Discord Rich Presence"] = "False";
                SettingsParser.WriteFile(SettingsFile, Settings);
                DiscordRichPresence.RPCDeInitialize();
                #endregion
            }
        }

    }
}
