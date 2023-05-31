﻿using DiscordRPC;

namespace Mod_Manager_V2.Resources
{
    internal class DiscordRichPresence
    {
        #region RPC Initalize
        public static DiscordRpcClient RPCClient;

        public static void RPCInitialize()
        {
            RPCClient = new DiscordRpcClient("1101958992328142959");

            RPCClient.SetPresence(new RichPresence()
            {
                State = "Downloading Mods",
                Assets = new Assets()
                {
                    LargeImageKey = "forzamods"
                },

                Buttons = new Button[]
                {
                    new Button() { Label = "Discord Link", Url = "https://discord.gg/forzamods"},
                    new Button() { Label = "Our Website", Url = "https://forzamods.github.io/website"}
                }
            });

            RPCClient.Initialize();
        }
        #endregion

        #region RPC De-Initialize
        public static void RPCDeInitialize()
        {
            RPCClient.Dispose();
        }
        #endregion
    }
}