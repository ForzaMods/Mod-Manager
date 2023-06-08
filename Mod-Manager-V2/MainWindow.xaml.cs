using System;
using System.Windows;
using IniParser.Model;
using IniParser;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Diagnostics;

namespace Mod_Manager_V2
{
    public partial class MainWindow : MetroWindow
    {
        public List<ModPage> ModPages { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            SettingsFile.CreateSettingsFile();
            #region Path Checking shit
            /*string ShittySettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(ShittySettingsFile);
            if (!bool.Parse(Settings["Settings"]["Usermode"])) { CheckForPath.CheckIfFolderExists(); } else { CheckForAdmin.FirstLaunch(); }*/
            #endregion
            SettingsFile.CheckForDiscordRPC();
            ModPages = new List<ModPage>();

            ModPages.Add(new ModPage
            {
                Id = 1,
                Name = "Mod 1",
                Version = "6.0",
                Creator = "afatcock#1234",
                Description = "This is Mod 1",
                ImageLink = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.readersdigest.ca%2Fwp-content%2Fuploads%2F2019%2F11%2Fcat-10-e1573844975155.jpg&f=1&nofb=1&ipt=38c2a3a9ca86eb6a231b3839c7e1668636dd79392cde7193f0b5fa34d487ff08&ipo=images",
                FileLink = "https://example.com/mod1.zip",
                UploadDate = "69/69/69",
                FilePaths = new string[] { "path/to/file1", "path/to/file2" }
            });

            ModPages.Add(new ModPage
            {
                Id = 2,
                Name = "Mod 2",
                Version = "7.3",
                Creator = "nuigggasfatcock#5678",
                Description = "This is Mod 2asdasdasdasda",
                ImageLink = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fthewowstyle.com%2Fwp-content%2Fuploads%2F2015%2F04%2FGray-Cat-MorgueFile-Nov16th-2013.jpg&f=1&nofb=1&ipt=6aedfc7e87fa4d6c4fccabb129d62463ec1ef4c3040d62c3993c20d78992e578&ipo=images",
                FileLink = "https://example.com/mod2.zip",
                UploadDate = "69/69/69",
                FilePaths = new string[] { "path/to/file3", "path/to/file4", "path/to/file5" }
            });

            ModPages.Add(new ModPage
            {
                Id = 3,
                Name = "Mod 3",
                Version = "1.2",
                Creator = "zfatcock#5678",
                Description = "This is Mod 3",
                ImageLink = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fthewowstyle.com%2Fwp-content%2Fuploads%2F2015%2F04%2FGray-Cat-MorgueFile-Nov16th-2013.jpg&f=1&nofb=1&ipt=6aedfc7e87fa4d6c4fccabb129d62463ec1ef4c3040d62c3993c20d78992e578&ipo=images",
                FileLink = "https://example.com/mod2.zip",
                UploadDate = "69/69/69",
                FilePaths = new string[] { "path/to/file3", "path/to/file4", "path/to/file5" }
            });

            ModPages.Add(new ModPage
            {
                Id = 4,
                Name = "Mod 4",
                Version = "8.9",
                Creator = "bfatcock#5678",
                Description = "This is Mod 4",
                ImageLink = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fthewowstyle.com%2Fwp-content%2Fuploads%2F2015%2F04%2FGray-Cat-MorgueFile-Nov16th-2013.jpg&f=1&nofb=1&ipt=6aedfc7e87fa4d6c4fccabb129d62463ec1ef4c3040d62c3993c20d78992e578&ipo=images",
                FileLink = "https://example.com/mod2.zip",
                UploadDate = "69/69/69",
                FilePaths = new string[] { "path/to/file3", "path/to/file4", "path/to/file5" }
            });

            DataContext = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Settings_Button(object sender, RoutedEventArgs e)
        {
            if (!SettingsVariables.SettingsStatus) { SettingsVariables.SettingsWindow.Show(); SettingsVariables.SettingsStatus = true; }
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            // seems to be faster ig?
            Environment.Exit(1);
        }

        private void DraggingFunctionality(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Button categoryButton = (Button)sender;
            string category = categoryButton.Content.ToString();
            List<ModPage> sortedModPages;

            switch (category)
            {
                case "Default":
                    sortedModPages = ModPages; // no sorting needed, display all mod pages
                    break;
                case "Name":
                    sortedModPages = ModPages.OrderBy(m => m.Name).ToList(); // sort by name
                    break;
                case "Version":
                    sortedModPages = ModPages.OrderBy(m => m.Version).ToList(); // sort by version
                    break;
                case "Creator":
                    sortedModPages = ModPages.OrderBy(m => m.Creator).ToList(); // sort by creator
                    break;
                case "Upload Date":
                    sortedModPages = ModPages.OrderBy(m => m.UploadDate).ToList(); // sort by upload date
                    break;
                default:
                    sortedModPages = ModPages; // needs to be here or the updating fucks itself and default sorting is killed
                    break;
            }

            // update the itemssource of the itemscontrol to display the sorted mod pages
            modItemsControl.ItemsSource = sortedModPages;
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Button downloadButton = (Button)sender;
            ModPage modPage = downloadButton.DataContext as ModPage;

            // i will perform the download logic here using the "modpage.filelink"

        }
    }
}
