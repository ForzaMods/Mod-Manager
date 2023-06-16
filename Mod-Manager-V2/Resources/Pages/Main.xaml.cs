using IniParser.Model;
using IniParser;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mod_Manager_V2.Windows;
using System.IO;

namespace Mod_Manager_V2.Resources.Pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        private void RevertCRS_Click(object sender, RoutedEventArgs e)
        {
            var RenderScenariosPath = MainWindow.BaseDirectory + @"\media\RenderScenarios.zip";
            if (File.Exists(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip")) { File.Copy(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip", RenderScenariosPath); }
            else if (!File.Exists(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip"))
            {
                SettingsVariables.SettingsWindow.Hide();
                ErrorReportingVariables.ErrorReportingWindow.ErrorCode.Content = "Stock not found, do you wanna download?";
                ErrorReportingVariables.ErrorReportingWindow.Install.Visibility = Visibility.Visible;
                ErrorReportingVariables.ErrorReportingWindow.Show();
                ErrorReporting.InstallInt = 2;
            }
        }

        private void DiscordRPC_Toggled(object sender, RoutedEventArgs e)
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
    }
}
