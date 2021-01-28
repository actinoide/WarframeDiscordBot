using System;
using System.Net.Http;
using System.Text.Json;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace WarframeDiscordBot
{
    class Global
    {
        public static string tokenFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Actinoide\WarframeDiscordBotToken.txt";//filepath for the bots token
        public static string token;//bot token temporary storage
        public static DiscordSocketClient Client;//the client of the bot
        public static string[] Commands = new string[] { "help","fissures","invasions","events"};//list of available commands
        public static string Prefix = "test";//the prefix the bot responds to
    }
}
