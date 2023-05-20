using Gameloop.Vdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using Windows.Management.Deployment;
using Application = System.Windows.Application;

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
                var exePath = Process.GetCurrentProcess().MainModule.FileName;

                if (new WindowsPrincipal(WindowsIdentity.GetCurrent())
                    .IsInRole(WindowsBuiltInRole.Administrator))
                {
                    installPath();

                    // Set first launch to 0
                    string fileContent = File.ReadAllText(settingsFilePath);
                    fileContent = fileContent.Replace("First Launch = 1", "First Launch = 0");
                    File.WriteAllText(settingsFilePath, fileContent);

                    // Restart the app as user mode
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "explorer";
                    startInfo.Arguments = exePath;
                    startInfo.UseShellExecute = true;

                    Process.Start(startInfo);
                    Application.Current.Shutdown();
                }
                if (new WindowsPrincipal(WindowsIdentity.GetCurrent())
                    .IsInRole(WindowsBuiltInRole.User))
                {
                    // Restart the app as admin if "Usermode = 1"
                    if (SettingsPaths.SettingsPathRead.Contains("Usermode = 1"))
                    {
                        // Set test to 0
                        string fileContent = File.ReadAllText(settingsFilePath);
                        fileContent = fileContent.Replace("Usermode = 1", "");
                        File.WriteAllText(settingsFilePath, fileContent);

                        // Restart part
                        ProcessStartInfo startInfoUser = new ProcessStartInfo(exePath);
                        startInfoUser.Verb = "runas";
                        startInfoUser.Arguments = "restart";
                        startInfoUser.UseShellExecute = true;

                        Process.Start(startInfoUser);
                        Application.Current.Shutdown();
                    }
                }
            }
        }

        public static void installPath()
        {
            // Find the line with the "Game Install Path" setting
            string fileContent = File.ReadAllText(settingsFilePath);
            string gameInstallPathLine = fileContent
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(line => line.StartsWith("Game Install Path"));

            if (gameInstallPathLine != null)
            {
                // Extract the folder path after the "=" sign
                string folderPath = gameInstallPathLine.Substring(gameInstallPathLine.IndexOf('=') + 1).Trim();

                // Check if the folder exists
                bool folderExists = Directory.Exists(folderPath);

                if (!folderExists)
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
}
