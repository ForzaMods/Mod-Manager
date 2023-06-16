using IniParser.Model;
using IniParser;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Mod_Manager_V2.Resources.Pages
{
    /// <summary>
    /// Interaction logic for Path.xaml
    /// </summary>
    /// 

    public partial class Path : Page
    {
        bool isforzaopen = false;
        int PathCategory = 0;
        /*
         * 0 = default
         * 1 = auto
         * 2 = manual*/

        public Path()
        {
            InitializeComponent();
            Task.Run(ForzaBGWorker);
            PathDropBox.SelectedItem = PathDropBox.Items[0];
            Auto.Visibility = Visibility.Visible;
            TextA1.Visibility = Visibility.Visible;
            TextA2.Visibility = Visibility.Visible;
            TextA3.Visibility = Visibility.Visible;
            TextA4.Visibility = Visibility.Visible;
            Manual.Visibility = Visibility.Hidden;
            TextM1.Visibility = Visibility.Hidden;
            TextM2.Visibility = Visibility.Hidden;
            TextM3.Visibility = Visibility.Hidden;
            ForzaLabel.Visibility = Visibility.Visible;
            PathCategory = 1;
        }

        private void ForzaBGWorker()
        {
            Thread.Sleep(1500);
            while (true)
            {
                if (PathCategory == 1)
                {
                    Process[] processes = Process.GetProcessesByName("ForzaHorizon5");
                    if (processes.Length > 0)
                    {
                        Dispatcher.BeginInvoke((Action)delegate ()
                        {
                            ForzaLabel.Content = "You can get the path";
                            ForzaLabel.Foreground = System.Windows.Media.Brushes.Green;
                            PathButtonA.IsEnabled = true;
                        });

                        isforzaopen = true;
                    }
                    else
                    {
                        Dispatcher.BeginInvoke((Action)delegate ()
                        {
                            ForzaLabel.Content = "Open forza";
                            ForzaLabel.Foreground = System.Windows.Media.Brushes.Red;
                            PathButtonA.IsEnabled = false;
                        });

                        isforzaopen = false;
                    }
                }
            }
        }

        private void GetPath()
        {
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon5")[0];
            var Dir = TargetProcess.MainModule.FileName;
            var settingsFile = SettingsFile.BaseDirectory + @"\settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(settingsFile);
            Settings["Settings"]["Game Install Path"] = Dir;
            SettingsParser.WriteFile(settingsFile, Settings);
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            if (PathCategory == 1 && isforzaopen)
            {
                GetPath();
            }
            else if (PathCategory == 2)
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
        }

        private void PathDropBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            string selectedOption = selectedItem.Content.ToString();

            if (selectedOption == "Auto")
            {
                Auto.Visibility = Visibility.Visible;
                TextA1.Visibility = Visibility.Visible;
                TextA2.Visibility = Visibility.Visible;
                TextA3.Visibility = Visibility.Visible;
                TextA4.Visibility = Visibility.Visible;
                Manual.Visibility = Visibility.Hidden;
                TextM1.Visibility = Visibility.Hidden;
                TextM2.Visibility = Visibility.Hidden;
                TextM3.Visibility = Visibility.Hidden;
                ForzaLabel.Visibility = Visibility.Visible;
                PathButtonM.Visibility = Visibility.Hidden;
                PathButtonA.Visibility = Visibility.Visible;
                PathCategory = 1;
            }
            else if (selectedOption == "Manual")
            {
                Auto.Visibility = Visibility.Hidden;
                TextA1.Visibility = Visibility.Hidden;
                TextA2.Visibility = Visibility.Hidden;
                TextA3.Visibility = Visibility.Hidden;
                TextA4.Visibility = Visibility.Hidden;
                Manual.Visibility = Visibility.Visible;
                TextM1.Visibility = Visibility.Visible;
                TextM2.Visibility = Visibility.Visible;
                TextM3.Visibility = Visibility.Visible;
                ForzaLabel.Visibility = Visibility.Hidden;
                PathButtonM.Visibility = Visibility.Visible;
                PathButtonA.Visibility = Visibility.Hidden;
                isforzaopen = false;
                PathCategory = 2;
            }
        }
    }
}
