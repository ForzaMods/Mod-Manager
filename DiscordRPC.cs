using DiscordRPC;
using DiscordRPC.Logging;
using System.Windows;

namespace modmanager
{
    public partial class MainWindow : Window
    {
        public DiscordRpcClient client;

        void RPCInitalize()
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

        void RPCDeinitalize()
        {
            client.Dispose();
        }
    }
}
