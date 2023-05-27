using System;
using System.IO;
using IniParser;
using IniParser.Model;

namespace Mod_Manager_V2.Resources
{
    internal class SettingsFile
    {
        public static void CreateSettingsFile()
        {
            #region Strings And Bools
            string BaseDirectory = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mod Manager";
            string SettingsFile = BaseDirectory + @"\Settings.ini";
            bool MainFolderExists = File.Exists(BaseDirectory);
            bool OriginalFilesFolderExists = File.Exists(BaseDirectory + @"\Original Files");
            bool SettingsFileExists = File.Exists(SettingsFile);
            #endregion

            #region Create Files
            if (!MainFolderExists) { Directory.CreateDirectory(BaseDirectory); }

            if (!OriginalFilesFolderExists) { Directory.CreateDirectory(BaseDirectory + @"\Original Files"); }

            if (!SettingsFileExists) { using (File.Create(SettingsFile)) { } ; SetupSettingsFile(); }
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
            Settings["Settings"]["First Launch"] = "1";
            Settings["Settings"]["Usermode"] = "1";
            SettingsParser.WriteFile(SettingsFile, Settings);
            #endregion
        }
    }
}
