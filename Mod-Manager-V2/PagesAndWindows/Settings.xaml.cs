using IniParser.Model;
using IniParser;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using Mod_Manager_V2.Windows;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Mod_Manager_V2.PagesAndWindows
{
    public partial class Settings : Page
    {
        static ErrorReporting errorReporting = new ErrorReporting();

        public Settings()
        {
            InitializeComponent();
            PathChecking();
        }

        #region Path for the pirated fellas
        void PathChecking()
        {
            #region Strings and bools
            string SettingsFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            string pathvalue = Settings["Settings"]["Game Install Path"];
            #endregion

            #region Parse Settings File
            if (pathvalue == "Not Found")
            {
                PathInteraction.Visibility = Visibility.Visible;
                PathText.Visibility = Visibility.Visible;
                Task.Run(ForzaBGWorker);
            }
            #endregion
        }

        void ForzaBGWorker()
        {
            while (true)
            {
                Process[] process = Process.GetProcessesByName("ForzaHorizon5");
                if (process.Length > 0)
                {
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        ForzaLabel.Content = "You can get the path";
                        ForzaLabel.Foreground = System.Windows.Media.Brushes.Green;
                        PathButton.IsEnabled = true;
                    });
                }
                else
                {
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        ForzaLabel.Content = "Open forza";
                        ForzaLabel.Foreground = System.Windows.Media.Brushes.Red;
                        PathButton.IsEnabled = false;
                    });
                }
                Thread.Sleep(1500);
            }
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon5")[0];
            var Dir = TargetProcess.MainModule.FileName.Substring(0, TargetProcess.MainModule.FileName.LastIndexOf("\\"));
            var settingsFile = SettingsFile.BaseDirectory + @"\settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(settingsFile);
            Settings["Settings"]["Game Install Path"] = Dir;
            SettingsParser.WriteFile(settingsFile, Settings);
            PathInteraction.Visibility = Visibility.Hidden;
            PathText.Visibility = Visibility.Hidden;
            MessageBox.Show("Path successfully saved");
        }
        #endregion

        private void DiscordRPC_Toggled(object sender, RoutedEventArgs e)
        {
            #region Strings for settings file
            var toggleSwitch = (ToggleSwitch)sender;
            string SettingsFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager\Settings.ini";
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
            var RenderScenariosPath = MainWindow.BaseDir + @"\media\RenderScenarios.zip";
            if(MainWindow.BaseDir != "Not Found")
            {
                if (File.Exists(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip")) { File.Copy(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip", RenderScenariosPath); }
                else if (!File.Exists(SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip"))
                {
                    errorReporting.ErrorCode.Content = "Stock not found, do you wanna download?";
                    errorReporting.Install.Visibility = Visibility.Visible;
                    errorReporting.Show();
                }
            }
            else
            {
                MessageBox.Show("Path not found");
            }
        }

        private void RefreshPages_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mw.GetModPages();
            MainWindow.mw.GetDownloadedModPages();
        }
    }
}
