using IniParser.Model;
using IniParser;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using System;
using System.Net;
using System.Windows;
using Windows.Media.Core;
using System.Windows.Input;

namespace Mod_Manager_V2.Windows
{
    public partial class ErrorReporting : MetroWindow
    {
        public static int InstallInt = 0;
        /*
         * 0 = Default
         * 1 = CRS
         * 2 = Stock crs
         */
        public ErrorReporting()
        {
            InitializeComponent();
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Install_Click(object sender, RoutedEventArgs e)
        {
            var RenderScenariosPath = MainWindow.BaseDirectory + @"\media\RenderScenarios.zip";
            using (WebClient httpClient = new WebClient())
            {
                if (InstallInt == 1)
                {
                    httpClient.DownloadFile("https://cdn.discordapp.com/attachments/1034577082933592124/1097626438196269137/RenderScenarios.zip", RenderScenariosPath);
                    Hide();
                    MessageBox.Show("Downloaded");
                    InstallInt = 0;
                }
                if (InstallInt == 2)
                {
                    httpClient.DownloadFile("https://cdn.discordapp.com/attachments/1023231168532971671/1118996926248001678/RenderScenarios.zip", SettingsFile.BaseDirectory + @"\Original Files\RenderScenarios.zip");
                    Hide();
                    MessageBox.Show("Downloaded");
                    InstallInt = 0;
                }
            }
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string settingsFile = SettingsFile.BaseDirectory + @"\settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = SettingsParser.ReadFile(settingsFile);
                Settings["Settings"]["Game Install Path"] = dialog.SelectedPath;
                SettingsParser.WriteFile(settingsFile, Settings);
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
