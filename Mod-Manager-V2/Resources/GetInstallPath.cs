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

        public static string GetInstallPath(string packageName)
        {
            #region Get install path string
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
            #endregion
        }


        public static void InstallPath()
        {
            #region Check For Path
            dynamic libraryfolders = VdfConvert.Deserialize(File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\libraryfolders.vdf"));
            var installPathSteam = "";
            string installPathMS = "";
            foreach (var folder in libraryfolders.Value)
            {
                if (folder.ToString().Contains("\"1551360\""))
                {
                    installPathSteam = folder.Value.path.ToString() + @"\steamapps\common\ForzaHorizon5";
                    MessageBox.Show(folder.Value.path.ToString() + @"\steamapps\common\ForzaHorizon5");
                }
                if (installPathSteam == "")
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
            if (installPathSteam != "") { Settings["Settings"]["Game Install Path"] = installPathSteam; }
            else { Settings["Settings"]["Game Install Path"] = installPathMS; }
            SettingsParser.WriteFile(SettingsFile, Settings);
            #endregion
        }

        public static void CheckIfFolderExists()
        {

        }
    }
}
