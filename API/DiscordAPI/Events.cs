using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAPI
{
    public static class Events
    {
        public static string JoinServer(string Code)
        {
            Newtonsoft.Json.Linq.JObject Join=DiscordInterface.PostRequest("https://discordapp.com/api/v6/invite/"+Code,true);
            return (string)Join["channel"]["id"];
        }
        public static bool SendMessage(string ChannelID,string Message)
        {
            try{Newtonsoft.Json.Linq.JObject Mes = DiscordInterface.PostRequest("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages", "{\"content\":\"" + Message + "\",\"nonce\":\"618169420211337420\",\"tts\":false}", true);
                return true;}
            catch { return false; }
        }
        public static string CreateInvite(string ChannelID)
        {
            Newtonsoft.Json.Linq.JObject Inv = DiscordInterface.PostRequest("https://discordapp.com/api/v6/channels/" + ChannelID + "/invites", "{\"max_age\":null,\"max_uses\":0,\"temporary\":false}", true);
            return (string)Inv["code"];
        }
    }
}
