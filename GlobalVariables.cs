using System;
using System.IO;

namespace modmanager
{
    public static class SettingsVariables
    {
        public static bool isSettingsOpen { get; set; } = false;
    }

    public static class SettingsPaths
    {
        public static string SettingsPath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";
        public static string SettingsPathRead = File.ReadAllText(SettingsPath);
    }

    public static class ErrorReport
    {
        internal static readonly Window2 ErrorReporting = new Window2();
    }

    public static class PathNotFoundWindow
    {
        internal static readonly Window3 PathWindow = new Window3();
    }
}
