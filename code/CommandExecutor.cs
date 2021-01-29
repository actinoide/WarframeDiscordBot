using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace WarframeDiscordBot
{
    class CommandExecutor
    {
        public async Task Help(SocketMessage message)
        {
            await message.Channel.SendMessageAsync("this bot is currently under development. this command will be added later");//this feature will be added later
        }
        public async Task Fissures(SocketMessage message)
        {
            string receivedData = await SupportMethods.MakeAPIRequest("https://api.warframestat.us/pc/en/fissures", message);//makes an api call to get current data
            if (receivedData == null) return;//makes sure that something is returned to avoid errors in deserialization
            definitions.FissureDefinitions[] currentFissures;//initializing variable
            try//try deserializing 
            {
                currentFissures = JsonSerializer.Deserialize<definitions.FissureDefinitions[]>(receivedData);
            }
            catch (Exception e)//catches exeptions and lets the user now what happened
            {
                Console.WriteLine(e);
                await message.Channel.SendMessageAsync("JSON error. please let me(actinoide#6637) know");
                return;
            }
            string messageToSend = @"```";//initializing variable
            foreach (definitions.FissureDefinitions fissure in currentFissures)
            {//adding each fissures data to the string 
                messageToSend += fissure.tier + " " + fissure.missionType + " " + fissure.node +" " + fissure.enemy+ " " + fissure.eta + Environment.NewLine;
            }
            await message.Channel.SendMessageAsync(messageToSend + @"```");//sends the generated string as a discord message
            return;
        }
        public async Task Invasions(SocketMessage message)
        {
            string receivedData = await SupportMethods.MakeAPIRequest("https://api.warframestat.us/pc/en/invasions", message);//makes an api call to get current data
            if (receivedData == null) return;//makes sure that something is returned to avoid errors in deserialization
            definitions.InvasionDefinitions[] currentInvasions;//initializing variable
            try//try deserializing 
            {
                currentInvasions = JsonSerializer.Deserialize<definitions.InvasionDefinitions[]>(receivedData);
            }
            catch (Exception e)//catches exeptions and lets the user now what happened
            {
                Console.WriteLine(e);
                await message.Channel.SendMessageAsync("JSON error. please let me(actinoide#6637) know");
                return;
            }
            string messageToSend = @"```";//initializing variable
            foreach (definitions.InvasionDefinitions invasion in currentInvasions)
            {//adding each invasion data to the string 
                messageToSend += invasion.attackingFaction + " " + invasion.defendingFaction + " " + invasion.attackerReward.asString + " " + invasion.defenderReward.asString + " " + invasion.node + Environment.NewLine;
            }
            await message.Channel.SendMessageAsync(messageToSend + @"```");//sends the generated string as a discord message
            return;
        }
        public async Task Events(SocketMessage message)
        {
            string receivedData = await SupportMethods.MakeAPIRequest("https://api.warframestat.us/pc/en/events", message);//makes an api call to get current data
            if (receivedData == null) return;//makes sure that something is returned to avoid errors in deserialization
            definitions.EventDefinitions[] currentEvents;//initializing variable
            try//try deserializing 
            {
                currentEvents = JsonSerializer.Deserialize<definitions.EventDefinitions[]>(receivedData);
            }
            catch (Exception e)//catches exeptions and lets the user now what happened
            {
                Console.WriteLine(e);
                await message.Channel.SendMessageAsync("JSON error. please let me(actinoide#6637) know");
                return;
            }
            string messageToSend = @"```";//initializing variable
            foreach (definitions.EventDefinitions cevent in currentEvents)
            {//adding each fissures data to the string 
                messageToSend += cevent.description + " ";
                foreach(definitions.InterimSteps interimSteps in cevent.interimSteps)
                {
                    messageToSend += interimSteps.goal + ":" + interimSteps.reward.asString + " ";
                }
                messageToSend += cevent.maximumScore + ":";
                foreach (definitions.Rewards reward in cevent.rewards)
                {
                    messageToSend += reward.asString + " ";
                }
                messageToSend += Environment.NewLine;
            }
            await message.Channel.SendMessageAsync(messageToSend + @"```");//sends the generated string as a discord message
            return;
        }
        public async Task Prefix(SocketMessage message,string[] parameters)
        {
            // this command is currently not in use
            SocketGuildUser user = (SocketGuildUser)message.Author;//casts the message author as a user
            if (user.GuildPermissions.Administrator)//checks if the user has admin permisions
            {
                try//if the user has admin rights the prefix is set to the first argument
                {
                    Global.Prefix = parameters[1];
                }
                catch (IndexOutOfRangeException)//if the required argument isnt given we let the user now
                {
                    await message.Channel.SendMessageAsync("missing argument");
                    return;
                }
                catch (Exception e)//if anything else goes wrong the user is informed
                {
                    await message.Channel.SendMessageAsync("an error of type " + e + " occured. please let me (actinoide#6637)know");
                    return;
                }
                await message.Channel.SendMessageAsync("prefix succesfully changed to " + Global.Prefix);
                return;//if the proces is succesfull we inform the user and also let them know what the new prefix is
            }
            else
            { //if the user does not have the admin permissions the program informs them
                await message.Channel.SendMessageAsync("you have insufficent permissions to use this command");
            }
        }
    }
}
