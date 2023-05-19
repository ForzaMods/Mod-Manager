using Gameloop.Vdf;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows;
using Windows.Management.Deployment;

namespace modmanager
{
    
    public class InstallPath
    {

        public static string settingsFilePath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";

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

        public static void FirstLaunch()
        {
            if (SettingsPaths.SettingsPathRead.Contains("First Launch = 1"))
            {
                if (new WindowsPrincipal(WindowsIdentity.GetCurrent())
                    .IsInRole(WindowsBuiltInRole.Administrator))
                {
                    installPath();

                    // Set first launch to 0
                    string fileContent = File.ReadAllText(settingsFilePath);
                    fileContent = fileContent.Replace("First Launch = 1", "First Launch = 0");
                    File.WriteAllText(settingsFilePath, fileContent);

                    // Restart the app as user mode
                    var exeName = Environment.ProcessPath;
                    ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                    startInfo.UseShellExecute = true;
                    startInfo.Verb = "open";
                    Process.Start(startInfo);

                    Application.Current.Shutdown();
                }
                else
                {
                    ErrorReport.AllClose = true;

                    ErrorReport.ErrorReporting.errorcode.Content = "Please restart this app as admin mode";
                    ErrorReport.ErrorReporting.Show();
                }
            }
        }

        public static void installPath()
        {
            string fileContent = File.ReadAllText(settingsFilePath);

            if (fileContent.Contains("Game Install Path = Not Found"))
            {

                dynamic libraryfolders = VdfConvert.Deserialize(File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\libraryfolders.vdf"));
                var installpath = "";
                foreach (var folder in libraryfolders.Value)
                {
                    if (folder.ToString().Contains("\"1551360\""))
                    {
                        installpath = folder.Value.path.ToString() + @"\steamapps\common\ForzaHorizon5";
                        
                        string[] lines = File.ReadAllLines(settingsFilePath);
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Contains("Game Install Path"))
                            {
                                lines[i] = "Game Install Path = " + installpath;
                                break;
                            }
                        }

                        File.WriteAllLines(settingsFilePath, lines);
                    }
                }
                if (installpath == "")
                {
                    string packageName = "Microsoft.624F8B84B80_8wekyb3d8bbwe";
                    string installpathMS = GetInstallPath(packageName);

                    string[] lines = File.ReadAllLines(settingsFilePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("Game Install Path"))
                        {
                            lines[i] = "Game Install Path = " + installpathMS;
                            break;
                        }
                    }

                    File.WriteAllLines(settingsFilePath, lines); 
                }
            }
        }
    }
}
