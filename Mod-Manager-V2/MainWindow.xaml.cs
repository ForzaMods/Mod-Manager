using System;
using System.Windows;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Net;
using IniParser;

namespace Mod_Manager_V2
{
    public partial class MainWindow : MetroWindow
    {
        private ModPageParser modPageParser;
        public List<ModPage> modPages { get; set; }
        public static MainWindow mw;
        // Added this as test
        public static string BaseDirectory = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager";

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
            modPages = new List<ModPage>();
            modPageParser = new ModPageParser();
            mw = this;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<ModPage> modPages = await modPageParser.ParseModPagesFromGitHub();
            modItemsControl.ItemsSource = modPages;
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
            // shit doesnt work for some reason?

            Button categoryButton = (Button)sender;
            string category = categoryButton.Content.ToString();
            List<ModPage> sortedModPages;

            switch (category)
            {
                case "Default":
                    sortedModPages = modPages; // no sorting needed, display all mod pages
                    break;
                case "Name":
                    sortedModPages = modPages.OrderBy(m => m.Name).ToList(); // sort by name
                    break;
                case "Version":
                    sortedModPages = modPages.OrderBy(m => m.Version).ToList(); // sort by version
                    break;
                case "Creator":
                    sortedModPages = modPages.OrderBy(m => m.Creator).ToList(); // sort by creator
                    break;
                case "Upload Date":
                    sortedModPages = modPages.OrderBy(m => DateTime.Parse(m.UploadDate)).ToList(); // sort by upload date
                    break;
                default:
                    sortedModPages = modPages; // needs to be here or the updating fucks itself and default sorting is killed
                    break;
            }

            // update the items of the items to display the sorted mod pages
            modItemsControl.ItemsSource = sortedModPages;
        }

        #region Downloading (worky yayayayay)
        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Button downloadButton = (Button)sender;
            ModPage modPage = downloadButton.DataContext as ModPage;
            string Directory = modPage.Category;
            string DownloadPath;

            using (WebClient  httpClient = new WebClient())
            {
                string url = modPage.FileLink;
                string filename = getFilename(url);
                httpClient.DownloadFile(modPage.FileLink, BaseDirectory + "/" + filename);
            }

        }

        // Very nice working string from: https://ourcodeworld.com/articles/read/227/how-to-download-a-webfile-with-csharp-and-show-download-progress-synchronously-and-asynchronously
        private string getFilename(string hreflink)
        {
            Uri uri = new Uri(hreflink);

            string filename = System.IO.Path.GetFileName(uri.LocalPath);

            return filename;
        }
        #endregion
    }
}
