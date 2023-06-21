using System;
using System.Windows;
using MahApps.Metro.Controls;
using Mod_Manager_V2.Resources;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Net;
using Mod_Manager_V2.Windows;
using IniParser;
using IniParser.Model;

namespace Mod_Manager_V2
{
    public partial class MainWindow : MetroWindow
    {
        private ModPageParser modPageParser;
        public List<ModPage> modPages { get; set; }
        public static MainWindow mw;
        public static string BaseDir;
        static ErrorReporting errorReporting = new ErrorReporting();

        public MainWindow()
        {
            InitializeComponent();
            SettingsFile.CreateSettingsFile();
            #region Path Checking shit
            string ShittySettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(ShittySettingsFile);
            if (Settings["Settings"]["Usermode"] != "True") { CheckForPath.CheckIfFolderExists(); } else { CheckForAdmin.FirstLaunch(); }
            #endregion
            SettingsFile.CheckForDiscordRPC();
            modPages = new List<ModPage>();
            modPageParser = new ModPageParser();
            GetModPages();
            mw = this;
        }

        private async void GetModPages()
        {
            modPages = await modPageParser.ParseModPagesFromGitHub();
            modItemsControl.ItemsSource = modPages;
        }

        private void DraggingFunctionality(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Button categoryButton = (Button)sender;
            string? category = categoryButton.Content.ToString();
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
                    sortedModPages = modPages.OrderBy(m => m.UploadDate).ToList(); // sort by upload date
                    break;
                case "Installed":
                    sortedModPages = modPages.OrderBy(m => m.IsCRSRequired).ToList(); // this does fuck all rn and is just to compile
                    break;
                default:
                    sortedModPages = modPages; // needs to be here or the updating fucks itself and default sorting is killed
                    break;
            }

            // update the items of the itemscontrol to display the sorted mod pages
            modItemsControl.ItemsSource = sortedModPages;
        }

        #region Downloading
        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Button downloadButton = (Button)sender;
            ModPage? modPage = downloadButton.DataContext as ModPage;
            var Category = modPage.Category;
            var DownloadPath = "";

            if (BaseDir != "Not Found")
            {
                if (Category == "Car") { DownloadPath = BaseDir + @"\media\stripped\mediaoverride\rc0\cars"; }
                if (Category == "LibraryFolder") { DownloadPath = BaseDir + @"\media\stripped\mediaoverride\rc0\cars\_library"; }
                if (Category == "Else") { DownloadPath = BaseDir + modPage.FilePath; }

                using (WebClient httpClient = new WebClient())
                {
                    string? url = modPage.FileLink;
                    string? filename = getFilename(url);
                    httpClient.DownloadFile(url, DownloadPath + "/" + filename);
                    if (modPage.IsCRSRequired)
                    {
                        errorReporting.ErrorCode.Content = "This mod requires CRS. Do you wanna install?";
                        errorReporting.Install.Visibility = Visibility.Visible;
                        errorReporting.CRS = true;
                        errorReporting.Show();
                    }
                }
            }
            else if (BaseDir == "Not Found")
            {
                errorReporting.ErrorCode.Content = "Path not found, cannot install";
                errorReporting.PathButton.Visibility = Visibility.Visible;
                errorReporting.Show();
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

        private void HomeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SettingsFrame.Visibility = Visibility.Hidden;
            MWModPages.Visibility = Visibility.Visible;
            SortingButtons.Visibility = Visibility.Visible;
        }

        private void Settings_Button(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Visibility = Visibility.Visible;
            MWModPages.Visibility = Visibility.Hidden;
            SortingButtons.Visibility = Visibility.Hidden;
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Environment.Exit(1);
            }
        }
    }
}
