using System;
using System.IO;
using System.Windows;

namespace modmanager
{
    public partial class MainWindow : Window
    {
        private void makeFiles()
        {
            string folderPath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Directory.CreateDirectory(folderPath + "\\OriginalFiles");

                string filePath = folderPath + "\\settings.ini";

                if (!File.Exists(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("[Settings]");
                        writer.WriteLine("Theme = Dracula");
                        writer.WriteLine("Discord Rich Presence = True");
                        writer.WriteLine("Game Install Path = Not Found");
                    }
               }
            }
        }
    }
}