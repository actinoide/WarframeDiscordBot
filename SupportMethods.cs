using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WarframeDiscordBot
{
    class SupportMethods
    {
        public static async Task<string> MakeAPIRequest(string WebsiteAdress, SocketMessage Message)
        {
            HttpClient APIHandler = new HttpClient();//creates http client for the web request
            HttpResponseMessage CurrentData = new HttpResponseMessage();
            try//catching timeout issues (standard timeout is 100sec)
            {
                CurrentData = await APIHandler.GetAsync(WebsiteAdress);//requests data 
            }
            catch
            {
                await Message.Channel.SendMessageAsync("API call error. the target server is most likely overloaded or offline. please let me(actinoide#6637) know");//catches api errors
            }
            if (!CurrentData.IsSuccessStatusCode)//checks the returned status code
            {
                await Message.Channel.SendMessageAsync("html error ( codes starting with 5 are from the originating server and codes starting with 4 are related to the request). the following code was received : " + (int)CurrentData.StatusCode + " " + CurrentData.ReasonPhrase);//if the sttus code is not in the 200-299(success) range the user is informed
                return null;
            }
            APIHandler.Dispose();//disposes of the httphandler to free up ressources since it is no longer needed
            HttpContent ActualData = CurrentData.Content;//takes the content of the received data (removes header etc)
            string FinalData = await ActualData.ReadAsStringAsync();//converts the data to string
            return FinalData;
        }
    }
}
