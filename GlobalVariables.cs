using System;
using System.IO;
using System.Windows;

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

        internal static bool AllClose { get; set; }

        public static void CloseAll()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Window1 || window is MainWindow || window is Window2)
                {
                    window.Close();
                }
            }
        }
    }

    public static class PathNotFoundWindow
    {
        internal static readonly Window3 PathWindow = new Window3();
    }

    public static class Main
    {
        internal static readonly MainWindow mainWindow = new MainWindow();
    }

}
