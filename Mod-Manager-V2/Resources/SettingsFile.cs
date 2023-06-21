using System;
using System.IO;
using System.Windows;
using IniParser;
using IniParser.Model;
using Mod_Manager_V2.PagesAndWindows;
using Newtonsoft.Json.Linq;

namespace Mod_Manager_V2.Resources
{
    internal class SettingsFile
    {
        public static string? BaseDirectory;
        internal static readonly Settings settingsPage = new Settings();

        public static void CreateSettingsFile()
        {
            #region Strings And Bools
            BaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager";
            string SettingsFile = BaseDirectory + @"\Settings.ini";
            string LocalJson = BaseDirectory + @"\DownloadedMods.json";
            bool MainFolderExists = File.Exists(BaseDirectory);
            bool OriginalFilesFolderExists = File.Exists(BaseDirectory + @"\Original Files");
            bool SettingsFileExists = File.Exists(SettingsFile);
            bool LocalJsonExists = File.Exists(LocalJson);
            #endregion

            #region Create Files
            if (!MainFolderExists) { Directory.CreateDirectory(BaseDirectory); }

            if (!OriginalFilesFolderExists) { Directory.CreateDirectory(BaseDirectory + @"\Original Files"); }

            if (!SettingsFileExists) { using (File.Create(SettingsFile)) { }; SetupSettingsFile(); }

            if (!LocalJsonExists) { using (File.Create(LocalJson)) { }; SetupLocalJson(); }
            #endregion
        }

        public static void SetupSettingsFile()
        {
            #region Strings and vars
            string SettingsFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager\Settings.ini";
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

        public static void SetupLocalJson()
        {
            #region String
            // is like that else the file comes out all fucky and shit
string jsonString = @"
{
    ""downloadedmods"": [
        {
            ""id"": 0
        }
    ]
}";
            #endregion

            JObject json = JObject.Parse(jsonString);
            File.WriteAllText(BaseDirectory + @"\DownloadedMods.json", jsonString);
        }

        public static void CheckForDiscordRPC()
        {
            #region Strings and bools
            string SettingsFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mod Manager\Settings.ini";
            var SettingsParser = new FileIniDataParser();
            IniData Settings = SettingsParser.ReadFile(SettingsFile);
            string pathvalue = Settings["Settings"]["Game Install Path"];
            MainWindow.BaseDir = pathvalue;
            #endregion

            #region Parse Settings File
            if (Settings["Settings"]["Discord Rich Presence"].ToString() == "True")
            {
                try 
                { 
                    DiscordRichPresence.RPCInitialize(); 
                    settingsPage.DiscordRPC.IsOn = true; 
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            #endregion
        }
    }
}
