using System;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace modmanager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string settingFilePath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";
        public MainWindow()
        {
            InitializeComponent();
            makeFiles();


            //settings.ini shit
            if (File.Exists(settingFilePath))
            {
                string fileContent = File.ReadAllText(settingFilePath);
                if (fileContent.Contains("Discord Rich Presence = True"))
                {
                    DiscordRPC.IsChecked = true;
                }
            }
        }
        private void DiscordRPCTrue(object sender, RoutedEventArgs e)
        {
            RPCInitalize();

            string fileContent = File.ReadAllText(settingFilePath);
            fileContent = fileContent.Replace("Discord Rich Presence = False", "Discord Rich Presence = True");
            File.WriteAllText(settingFilePath, fileContent);
        }

        private void DiscordRPCFalse(object sender, RoutedEventArgs e)
        {
            RPCDeinitalize();

            string fileContent = File.ReadAllText(settingFilePath);
            fileContent = fileContent.Replace("Discord Rich Presence = True", "Discord Rich Presence = False");
            File.WriteAllText(settingFilePath, fileContent);
        }

        private void ExitButton(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeButton(object sender, EventArgs e)
        {
           WindowState = WindowState.Minimized;
        }

        private void DragWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
