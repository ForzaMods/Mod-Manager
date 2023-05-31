using Mod_Manager_V2.Windows;

namespace Mod_Manager_V2.Resources
{
    internal class SettingsVariables
    {
        public static bool SettingsStatus { get; set; } 

        internal static readonly Settings SettingsWindow = new Settings();
    }
    
    internal class ErrorReportingVariables
    {
        internal static readonly ErrorReporting ErrorReportingWindow = new ErrorReporting();
    }
}
