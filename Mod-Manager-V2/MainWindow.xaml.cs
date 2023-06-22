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
using System.IO;
using Newtonsoft.Json.Linq;

namespace Mod_Manager_V2
{
    public partial class MainWindow : MetroWindow
    {
        #region variables
        private ModPageParser modPageParser;
        public List<ModPage> modPages { get; set; }
        public List<ModPage> DownloadedModPages { get; set; }
        public List<ModPage> sortedModPages { get; set; }
        public static MainWindow mw;
        public static string BaseDir;
        static ErrorReporting errorReporting = new ErrorReporting();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            SettingsFile.CreateSettingsFile();
            #region Path Checking shit, local json.
            string ShittySettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(ShittySettingsFile);
            if (Settings["Settings"]["Usermode"] != "True") { CheckForPath.CheckIfFolderExists(); } else { CheckForAdmin.FirstLaunch(); }

            if(!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager\DownloadedMods.json"))
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager\DownloadedMods.json");
            #endregion
            #region vars
            modPages = new List<ModPage>();
            DownloadedModPages = new List<ModPage>();
            modPageParser = new ModPageParser();
            #endregion
            SettingsFile.CheckForDiscordRPC();
            GetModPages();

            try
            {
                JObject jsonObject = JObject.Parse(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager\DownloadedMods.json");
                JArray modsArray = (JArray)jsonObject["DownloadedMods"];
                GetDownloadedModPages();
            }
            catch
            {
                Installed.IsEnabled = false;
            }
            
            mw = this;
        }

        public async void GetModPages()
        {
            modPages = await modPageParser.ParseModPagesFromGitHub();
            modItemsControl.ItemsSource = modPages;
        }

        public async void GetDownloadedModPages()
        {
            DownloadedModPages = await modPageParser.ParseModPagesFromLocalJson();
            DWmodItemsControl.ItemsSource = DownloadedModPages;
        }

        private void DraggingFunctionality(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
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

                if(!File.Exists(DownloadPath))
                {
                    Directory.CreateDirectory(DownloadPath);
                }

                using (WebClient httpClient = new WebClient())
                {
                    string? url = modPage.FileLink;
                    string? filename = getFilename(url);
                    try
                    {
                        httpClient.DownloadFile(url, DownloadPath + "/" + filename);
                        if (modPage.IsCRSRequired)
                        {
                            errorReporting.ErrorCode.Content = "This mod requires CRS. Do you wanna install?";
                            errorReporting.Install.Visibility = Visibility.Visible;
                            errorReporting.CRS = true;
                            errorReporting.Show();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Download has failed.");
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

            string filename = Path.GetFileName(uri.LocalPath);

            return filename;
        }
        #endregion
        #region Buttons
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

        private void Installed_Click(object sender, RoutedEventArgs e)
        {
            GetDownloadedModPages();
            MWModPages.Visibility = Visibility.Hidden;
            DWModPages.Visibility = Visibility.Visible;
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
                default:
                    sortedModPages = modPages; // needs to be here or the updating fucks itself and default sorting is killed
                    break;
            }

            MWModPages.Visibility = Visibility.Visible;
            DWModPages.Visibility = Visibility.Hidden;
            modItemsControl.ItemsSource = sortedModPages;
        }

        private void UninstallButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion
    }
}
