using DiscordRPC;
using DiscordRPC.Logging;
using System.IO;
using System;

namespace modmanager
{
    public class DiscordRPC
    {
        public static DiscordRpcClient? client;

        public static void CheckForSetting()
        {
            string settingsFilePath = "C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\settings.ini";

            if (File.Exists(settingsFilePath))
            {
                string fileContent = File.ReadAllText(settingsFilePath);

                if (fileContent.Contains("Discord Rich Presence = True"))
                {
                    RPCInitalize();
                }
            }
        }

        public static void RPCInitalize()
        {

            client = new DiscordRpcClient("1101958992328142959");
            client.Logger = new ConsoleLogger() { Level = LogLevel.None };

            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                State = "A simple Forza Mod Manager",
                Assets = new Assets()
                {
                    LargeImageKey = "forzamods"
                },

                Buttons = new Button[]
                {
                    new Button() { Label = "Website", Url = "https://forzamods.github.io/website"},
                    new Button() { Label = "Discord Link", Url = "https://discord.gg/forzamods"}
                }
            });  
        }

        public static void RPCDeinitalize()
        {
            client?.Dispose();
        }
    }
}
