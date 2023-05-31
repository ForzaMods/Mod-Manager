using IniParser.Model;
using IniParser;
using System;
using System.Diagnostics;
using System.Windows;

namespace Mod_Manager_V2.Resources
{
    public class CheckForAdmin
    {
        public static void FirstLaunch()
        {
            #region Settings stuff, and vars for the shit
            var ExePath = Environment.ProcessPath;
            string SettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            #endregion

            #region Usermode part
            if (bool.Parse(Settings["Settings"]["First Launch"]) && bool.Parse(Settings["Settings"]["User Mode"]))
            {
                #region Replace "Usermode" string
                Settings["Settings"]["User Mode"] = "False";
                SettingsParser.WriteFile(SettingsFile, Settings);
                #endregion

                #region Restart as admin
                ProcessStartInfo startInfoUser = new ProcessStartInfo(ExePath);
                startInfoUser.Verb = "runas";
                startInfoUser.Arguments = "restart";
                startInfoUser.UseShellExecute = true;

                Process.Start(startInfoUser);
                Application.Current.Shutdown();
                #endregion
            }
            #endregion

            #region Admin part
            if (bool.Parse(Settings["Settings"]["First Launch"]) && !bool.Parse(Settings["Settings"]["User Mode"]))
            {
                CheckForPath.CheckIfFolderExists();

                #region Replace "First Launch" string
                Settings["Settings"]["First Launch"] = "False";
                SettingsParser.WriteFile(SettingsFile, Settings);
                #endregion

                #region Restart as usermode
                Process.Start("explorer.exe", ExePath);
                Application.Current.Shutdown();
                #endregion
            }
            #endregion
        }
    }
}
