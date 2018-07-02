using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAPI
{
    public static class Events
    {
        public static string MYID = "458279302379864067";
        public delegate void HandlerType(string Message, string SenderID,string ChannelID);
        public static void Start()
        {
            Config.LoadConfig();
            DiscordInterface.SignIn();
        }
        public static string JoinServer(string Code)
        {
            Newtonsoft.Json.Linq.JObject Join=DiscordInterface.PostRequest("https://discordapp.com/api/v6/invite/"+Code,true,"POST");
            return (string)Join["guild"]["id"];
        }
        public static string JoinDM(string Code)
        {
            Newtonsoft.Json.Linq.JObject Join = DiscordInterface.PostRequest("https://discordapp.com/api/v6/invite/" + Code, true, "POST");
            return (string)Join["channel"]["id"];
        }
        public static bool SendMessage(string ChannelID,string Message)
        {
            try{
                Newtonsoft.Json.Linq.JObject Mes = DiscordInterface.PostRequest("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages", "{\"content\":\"" + Message + "\",\"nonce\":\"618169420211337420\",\"tts\":false}", true, "POST");
                return true;}
            catch { return false; }
        }
        public static string CreateInvite(string ChannelID)
        {
            Newtonsoft.Json.Linq.JObject Inv = DiscordInterface.PostRequest("https://discordapp.com/api/v6/channels/" + ChannelID + "/invites", "{\"max_age\":0,\"max_uses\":0,\"temporary\":false}", true, "POST");
            return (string)Inv["code"];
        }
        public static string CreateDMInvite(string ChannelID)
        {
            Newtonsoft.Json.Linq.JObject Inv = DiscordInterface.PostRequest("https://discordapp.com/api/v6/channels/" + ChannelID + "/invites","{}", true, "POST");
            return (string)Inv["code"];
        }
        public static Newtonsoft.Json.Linq.JObject GetMessages(string ChannelID)
        {
            return DiscordInterface.PostRequest("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages?limit=10", true, "GET");
        }
        public static Newtonsoft.Json.Linq.JArray GetTextChannels(string GuildID)
        {
            Newtonsoft.Json.Linq.JArray Rooms = Newtonsoft.Json.Linq.JArray.Parse("[]");
            Newtonsoft.Json.Linq.JObject RoomData = GetAllChannels(GuildID);
            foreach (Newtonsoft.Json.Linq.JObject Room in RoomData["Content"])
            {
                if ((int)Room["type"] == 0)
                {
                    Rooms.Add(Room);
                }
            }
            return Rooms;
        }
        public static Newtonsoft.Json.Linq.JObject GetAllChannels(string GuildID)
        {
            return DiscordInterface.PostRequest("https://discordapp.com/api/v6/guilds/" + GuildID + "/channels", true, "GET");
        }
        public static void LeaveServer(string GuildID)
        {
            DiscordInterface.PostRequest("https://discordapp.com/api/v6/users/@me/guilds/" + GuildID, true,"DELETE");
        }
    }
}
