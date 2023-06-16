using System;
using System.IO;
using IniParser;
using IniParser.Model;

namespace Mod_Manager_V2.Resources
{
    internal class SettingsFile
    {
        public static string BaseDirectory;

        public static void CreateSettingsFile()
        {
            #region Strings And Bools
            BaseDirectory = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager";
            string SettingsFile = BaseDirectory + @"\Settings.ini";
            bool MainFolderExists = File.Exists(BaseDirectory);
            bool OriginalFilesFolderExists = File.Exists(BaseDirectory + @"\Original Files");
            bool SettingsFileExists = File.Exists(SettingsFile);
            #endregion

            #region Create Files
            if (!MainFolderExists) { Directory.CreateDirectory(BaseDirectory); }

            if (!OriginalFilesFolderExists) { Directory.CreateDirectory(BaseDirectory + @"\Original Files"); }

            if (!SettingsFileExists) { using (File.Create(SettingsFile)) { }; SetupSettingsFile(); }
            #endregion
        }

        public static void SetupSettingsFile()
        {
            #region Strings and vars
            string SettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = new IniData();
            #endregion

            #region Setup Settings File
            Settings["Settings"]["Discord Rich Presence"] = "True";
            Settings["Settings"]["Game Install Path"] = "Not Found";
            Settings["Settings"]["Usermode"] = "True";
            SettingsParser.WriteFile(SettingsFile, Settings);
            #endregion
        }

        public static void CheckForDiscordRPC()
        {
            #region Strings and bools
            string SettingsFile = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            string pathvalue = Settings["Settings"]["Game Install Path"];
            MainWindow.BaseDirectory = pathvalue;
            #endregion

            #region Parse Settings File
            if (Settings["Settings"]["Discord Rich Presence"].ToString() == "True")
            {
                try { DiscordRichPresence.RPCInitialize(); SettingsVariables.SettingsPage.DiscordRPC.IsOn = true; }
                catch (Exception ex) { ErrorReportingVariables.ErrorReportingWindow.ErrorCode.Content = ex.Message; ErrorReportingVariables.ErrorReportingWindow.Show(); }
            }
            #endregion
        }
    }
}
