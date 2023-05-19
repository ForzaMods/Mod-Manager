using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace modmanager
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


            MakeFiles.makeFolders();
            MakeFiles.makeSettingsFile();
            InstallPath.FirstLaunch();
            DiscordRPC.CheckForSetting();

            // Drag and drop
            PreviewDragOver += MW_PDO;
            Drop += MW_Drop;


        }

        private void MW_PDO(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        } 

        private void MW_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (Path.GetExtension(file).ToLower() == ".zip")
                    {
                        MessageBox.Show("aaaaa");
                    }
                }
            }
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
 