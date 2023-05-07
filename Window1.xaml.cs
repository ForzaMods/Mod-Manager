using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace modmanager
{
    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
            Checking();
        }

        private void SettingsClose(object sender, RoutedEventArgs e)
        {
            SettingsVariables.isSettingsOpen = false;
            Close();
        }

        public void Checking()
        {
            string gameInstallPath = null;

            string settingsFilePath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";

            //Discord RPC
            string fileContent = File.ReadAllText(settingsFilePath);

            if (fileContent.Contains("Discord Rich Presence = True"))
            {
                DiscordRPCButton.IsChecked = true;
            } 
            
            //Install Path
            if (fileContent.Contains("Game Install Path = Not Found"))
            {
                InstallPathLabel.Content = "Path not found, you are probably on a cracked version";
                InstallPathLabel.FontSize = 15;
                InstallPathLabel.Height = 35;
            }
            else
            { 
                string[] lines = File.ReadAllLines(settingsFilePath);

                foreach (string line in lines)
                {
                    if(line.StartsWith("Game Install Path"))
                    {
                        gameInstallPath = line.Substring(line.IndexOf('=') + 1).Trim();
                        break;
                    }
                }

                InstallPathLabel.Content = gameInstallPath;
            }
        }




        private void DragWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private void DiscordRPCTrue(object sender, RoutedEventArgs e)
        {
            DiscordRPC.RPCInitalize();

            string settingsFilePath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";
            string fileContent = File.ReadAllText(settingsFilePath);
            fileContent = fileContent.Replace("Discord Rich Presence = False", "Discord Rich Presence = True");
            File.WriteAllText(settingsFilePath, fileContent);
        }

        private void DiscordRPCFalse(object sender, RoutedEventArgs e)
        {
            DiscordRPC.RPCDeinitalize();

            string settingsFilePath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";
            string fileContent = File.ReadAllText(settingsFilePath);
            fileContent = fileContent.Replace("Discord Rich Presence = True", "Discord Rich Presence = False");
            File.WriteAllText(settingsFilePath, fileContent);
        }

        private void PathButton(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                InstallPathLabel.FontSize = 10;
                InstallPathLabel.Content = dialog.SelectedPath;

                string settingsFilePath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";
                string[] lines = File.ReadAllLines(settingsFilePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("Game Install Path"))
                    {
                        lines[i] = "Game Install Path = " + dialog.SelectedPath; 
                        break;
                    }
                }

                File.WriteAllLines(settingsFilePath, lines);
            }
        }

    }
}
