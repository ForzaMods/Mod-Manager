using System;
using System.IO;

namespace modmanager
{
    public class MakeFiles
    {
        public static string folderPath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager";
        public static void makeFolders()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Directory.CreateDirectory(folderPath + "\\OriginalFiles");
            }
        }

        public static void makeSettingsFile()
        {
            string filePath = folderPath + "\\settings.ini";

            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("[Settings]");
                    writer.WriteLine("Discord Rich Presence = True");
                    writer.WriteLine("Game Install Path = Not Found");
                    writer.WriteLine("First Launch = 1");
                }
            }
        }
    }
}