using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Windows.Management.Deployment;
using Gameloop.Vdf;


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
        public class UwpAppInstaller
        {
            public static string GetInstallPath(string packageName)
            {
                PackageManager packageManager = new PackageManager();
                var packages = packageManager.FindPackages();
                foreach (var package in packageManager.FindPackages())
                {
                    if (package.Id.FamilyName == packageName)
                    {
                        return package.InstalledLocation.Path;
                    }
                }

                return null;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //check install path for steam
            dynamic libraryfolders = VdfConvert.Deserialize(File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\libraryfolders.vdf"));
            var installpath = "";
            foreach (var folder in libraryfolders.Value)
            {
                if (folder.ToString().Contains("\"1551360\""))
                {
                    installpath = folder.Value.path.ToString() + @"\steamapps\common\ForzaHorizon5";
                    MessageBox.Show(folder.Value.path.ToString() + @"\steamapps\common\ForzaHorizon5");
                }
            }
            if (installpath == "")
            {
                string packageName = "Microsoft.624F8B84B80_8wekyb3d8bbwe";
                string installPath = UwpAppInstaller.GetInstallPath(packageName);
                MessageBox.Show(installPath);
            }

        }
    }
}
