using System;
using System.Windows;
using System.Windows.Input;

namespace modmanager
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MakeFiles.makeFiles();
            InstallPath.installPath();
            DiscordRPC.CheckForSetting();
            
        }

        private void ExitButton(object sender, EventArgs e)
        {
            Window1 settings = new Window1();
            settings.Close();
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

        private void Settings_Open(object sender, RoutedEventArgs e)
        { 
            if (!SettingsVariables.isSettingsOpen)
            {
                Window1 settings = new Window1();
                settings.Show();
                settings.Topmost = true;
                SettingsVariables.isSettingsOpen = true;
            }
        }
    }
}
