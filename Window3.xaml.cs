using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace modmanager
{
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }


        private void ClosePathError(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DragWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SetPath(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
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
