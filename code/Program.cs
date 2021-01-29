using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;

namespace WarframeDiscordBot
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("starting bot");//console output for the bot operator
            if (File.Exists(Global.tokenFilePath))//checks if the file storing the token exists
            {
                Global.token = File.ReadAllText(Global.tokenFilePath);//reads the token
                if (Global.token.Length == 0)//if the file is empty
                {
                    Console.WriteLine(@"token file is empty. plese change the content of the file in appdata\Roaming\Actinoide\WarframeDiscordBotToken.txt to the token of the bot and restart this application.");//the user is informed
                    Console.ReadKey();//giving the user time to read the info
                    return;//closing the application    
                }
            }
            else //if the file doesnt exist
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Actinoide"))//we check if the directory exists
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Actinoide");//and create it if it doesnt
                }
                File.Create(Global.tokenFilePath);//then we create the file and let the user know
                Console.WriteLine(@"token file not found. created new token file. please  change the content of the file in appdata\Roaming\Actinoide\WarframeDiscordBotToken.txt to the token of the bot and restart this application.");
                Console.ReadKey();//giving the user time to read the info
                return;//closing the application
            }
            new Program().MainAsync().GetAwaiter().GetResult();//initializing our asynchronous system
        }
        public async Task MainAsync()//async launching method (called in main)
        {
            Global.Client = new DiscordSocketClient();//sets the client to an actual instance
            try//try catch to not crash when the given token is invalid
            {
                await Global.Client.LoginAsync(TokenType.Bot, Global.token);//logs into the bot account and starts the connection to the discord server
            }
            catch
            {
                Console.WriteLine("invalid token. please check the token file to confirm that it contains the correct token and nothing else. should the issue reapear let me(actinoide#6637)know.");//in case of invalid token we let the user know
                Console.ReadKey();//giving user time to read info
                return;//closing program
            }
            await Global.Client.SetActivityAsync(new Game("adress using: " + Global.Prefix, ActivityType.Playing));//setting the bots activity
            await Global.Client.StartAsync();
            Global.Client.MessageReceived += TaskHandlerAsync;//adds an eventhandler to the message received event
            Console.WriteLine("bot initalized");//tells the bot operator that the bot is now initialized and ready for use
            await Task.Delay(Timeout.Infinite);//waits for an infinite amount to not finish the method which would cause main to end and the app to close
        }
        public async Task TaskHandlerAsync(SocketMessage message)//event for when a message is received
        {
            if (message.Author.IsBot) return;//to avoid the bot responding to itself or other bots it returns if the message was sent by a bot
            foreach (string command in Global.Commands)//goes through all commands
            {
                if (message.Content.ToLower().StartsWith(Global.Prefix + command))//checks if the message starts with the prefix and command
                {
                    await ExecuteCommand(message, command);//if it does the correct command is executed
                    break;
                }
            }
        }
        public async Task ExecuteCommand(SocketMessage message, string commandName)//method that executes commands
        {
            string[]    parameters = message.Content.Split(' ');//splits the reeived text into a string array [0] is the prefix and command [1+] are any other arguments passed to the bot
            CommandExecutor Executor = new CommandExecutor();
            switch (commandName)//checks which command should be executed and executes the command
            {
                case "help":
                    await Executor.Help(message);
                    break;
                case "fissures":
                    await Executor.Fissures(message);
                    break;
                case "invasions":
                    await Executor.Invasions(message);
                    break;
                case "events":
                    await Executor.Events(message);
                    break;
                /*
                case "prefix":
                    await Executor.Prefix(message, parameters);
                    break;
                */
                default:
                    Console.WriteLine("invalid command in program.testcommand. please let me(actinoide#6637) know");
                    break;
            }
        }
    }
}