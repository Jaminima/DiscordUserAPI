using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace DiscordAPI
{
    public static class DiscordInterface
    {
        static string Token = "";

        public static Boolean SignIn()
        {
            try { Token = (string)PostRequest("https://discordapp.com/api/v6/auth/login", "{\"email\":\"" + Config.CurConfig["SignIn"]["Email"] + "\",\"password\":\"" + Config.CurConfig["SignIn"]["Password"] + "\"}", false, "POST")["token"]; return true; }
            catch { return false; }
        }
        
        public static Newtonsoft.Json.Linq.JObject PostRequest(string URL,Boolean bToken,string Type)
        {
            WebRequest Req = WebRequest.Create(URL);
            Req.Method = Type;
            Req.ContentType = "application/json";
            if (bToken) { Req.Headers.Add("authorization", Token); }
            if (Type == "POST")
            {
                Req.GetRequestStream().Write(new byte[] { }, 0, new byte[] { }.Length);
                Req.GetRequestStream().Close();
            }
            WebResponse Res = Req.GetResponse();
            string D = new StreamReader(Res.GetResponseStream()).ReadToEnd();
            if (Type == "DELETE") { return null; }
            if (D[0].ToString() == "[") { return (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject("{Content:"+D+"}"); } else { return Newtonsoft.Json.Linq.JObject.Parse(D); }
        }

        public static Newtonsoft.Json.Linq.JObject PostRequest(string URL,string Data,Boolean bToken, string Type)
        {
            byte[] bData = Encoding.ASCII.GetBytes(Data);
            WebRequest Req = WebRequest.Create(URL);
            Req.Method = Type;
            Req.ContentType = "application/json";
            if (bToken) { Req.Headers.Add("authorization", Token); }
            Req.GetRequestStream().Write(bData, 0, bData.Length);
            Req.GetRequestStream().Close();
            WebResponse Res = Req.GetResponse();
            return Newtonsoft.Json.Linq.JObject.Parse(new StreamReader(Res.GetResponseStream()).ReadToEnd());
        }

        public static void Listen(string ChannelID,Events.HandlerType Handler)
        {
            Newtonsoft.Json.Linq.JObject Previous = Newtonsoft.Json.Linq.JObject.Parse("{}");
            while (true)
            {
                Newtonsoft.Json.Linq.JObject New = Events.GetMessages(ChannelID);
                if (Previous.ToString() != New.ToString())
                {
                    Handler((string)New["Content"][0]["content"], (string)New["Content"][0]["author"]["id"],(string)New["Content"][0]["channel_id"]);
                    Previous = New;
                }
                System.Threading.Thread.Sleep(10);
            }
        }

    }
}
