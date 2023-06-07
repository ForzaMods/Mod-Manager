using IniParser.Model;
using IniParser;
using System;
using System.Diagnostics;
using System.Windows;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Mod_Manager_V2.Resources
{
    public class CheckForAdmin
    {
        #region Bools
        public static bool UsermodeBool()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.User);
        }

        public static bool AdminModeBool()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        #endregion

        #region First Launch method
        public static void FirstLaunch()
        {
            #region Settings stuff, and vars for the shit
            var ExePath = Environment.ProcessPath;
            string SettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            #endregion

            #region Usermode part
            if (UsermodeBool() && bool.Parse(Settings["Settings"]["Usermode"]))
            {
                #region Replace "Usermode" string
                Settings["Settings"]["Usermode"] = "False";
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
            if (AdminModeBool() && !bool.Parse(Settings["Settings"]["Usermode"]))
            {
                Task.Run(() => CheckForPath.InstallPath());

                #region Restart as usermode
                Process.Start("explorer.exe", ExePath);
                Application.Current.Shutdown();
                #endregion
            }
            #endregion
        }
        #endregion
    }
}
