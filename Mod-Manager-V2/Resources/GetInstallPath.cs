using Gameloop.Vdf;
using IniParser;
using IniParser.Model;
using System;
using System.IO;
using System.Windows;
using Windows.Management.Deployment;

namespace Mod_Manager_V2.Resources
{
    public class CheckForPath
    {
        #region Get install path string
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
        #endregion

        public static void InstallPath()
        {
            #region Check For Path
            dynamic libraryfolders = VdfConvert.Deserialize(File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\libraryfolders.vdf"));
            string installPathSteam = "nothing";
            string installPathMS = "nothing";
            foreach (var folder in libraryfolders.Value)
            {
                if (folder.ToString().Contains("\"1551360\""))
                {
                    installPathSteam = folder.Value.path.ToString() + @"\steamapps\common\ForzaHorizon5";
                    MessageBox.Show(folder.Value.path.ToString() + @"\steamapps\common\ForzaHorizon5");
                }
                if (installPathSteam == "nothing")
                {
                    string packageName = "Microsoft.624F8B84B80_8wekyb3d8bbwe";
                    installPathMS = GetInstallPath(packageName);
                }
            }
            #endregion

            #region Strings for settings file
            string SettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            #endregion

            #region Save it to the settings file
            try
            {
                if (installPathSteam != null && !installPathSteam.Equals("nothing")) { Settings["Settings"]["Game Install Path"] = installPathSteam; }
                SettingsParser.WriteFile(SettingsFile, Settings);
            }
            catch { }

            try
            {
                if (installPathMS != null && !installPathMS.Equals("nothing")) { Settings["Settings"]["Game Install Path"] = installPathMS; }
                SettingsParser.WriteFile(SettingsFile, Settings);
            }
            catch { }
            #endregion
        }

        public static void CheckIfFolderExists()
        {
            #region Settings stuff
            string settingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(settingsFile);
            string pathvalue = Settings["Settings"]["Game Install Path"];
            bool FolderExists = File.Exists(pathvalue);
            #endregion

            if(!FolderExists && !pathvalue.Equals("Not Found"))
            {
                Settings["Settings"]["Usermode"] = "True";
                SettingsParser.WriteFile(settingsFile, Settings);
                CheckForAdmin.FirstLaunch();
            }
        }
    }
}
