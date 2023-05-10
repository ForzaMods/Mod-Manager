using System;
using System.Collections.Generic;
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
            MakeFiles.makeSettingsFile();
            InstallPath.installPath();
            DiscordRPC.CheckForSetting();
            
        }

        private void ExitButton(object sender, EventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Window1 || window is MainWindow || window is Window2)
                {
                    window.Close();
                }
            }
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
 