using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DiscordUserAPI.Discord
{
    public class Actions
    {
        public NetworkInterface NetworkInterface;

        public Actions(NetworkInterface networkInterface) { NetworkInterface = networkInterface; }

        public string JoinGuild(string InviteCode)
        {
            JToken Res = NetworkInterface.Request("invite/" + InviteCode, WToken: true);
            if (Res != null) { return Res["guild"]["id"].ToString(); }
            return null;
        }

        public string JoinGroup(string InviteCode)
        {
            JToken Res = NetworkInterface.Request("invite/" + InviteCode, WToken: true);
            if (Res != null) { return Res["channel"]["id"].ToString(); }
            return null;
        }

        public void LeaveGuild(string GuildID)
        {
            NetworkInterface.Request("users/@me/guilds/" + GuildID, WToken: true, Method: "DELETE");
        }

        public bool GuildExists(string InviteCode)
        {
            JToken Res = NetworkInterface.Request("invite/" + InviteCode + "?with_counts=true", Method: "GET");
            return Res != null;
        }

        public string SendMessage(string ChannelID, string Message)
        {
            JToken Res = NetworkInterface.Request("channels/" + ChannelID + "/messages", "{\"content\":\"" + Message + "\",\"nonce\":\"" + Master.Rnd.Next(0, int.MaxValue) + "\",\"tts\":false}", true);
            if (Res != null) { return Res["id"].ToString(); }
            return null;
        }

        public string CreateInvite(string ChannelID)
        {
            JToken Res = NetworkInterface.Request("channels/" + ChannelID + "/invites", "{\"max_age\":0,\"max_uses\":0,\"temporary\":false}", true);
            if (Res != null) { return Res["code"].ToString(); }
            return null;
        }
    }
}
