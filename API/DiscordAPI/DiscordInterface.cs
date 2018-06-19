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
            try { Token = (string)PostRequest("https://discordapp.com/api/v6/auth/login", "{\"email\":\"" + Config.CurConfig["SignIn"]["Email"] + "\",\"password\":\"" + Config.CurConfig["SignIn"]["Password"] + "\"}", false)["token"]; return true; }
            catch { return false; }
        }
        
        public static Newtonsoft.Json.Linq.JObject PostRequest(string URL,Boolean bToken)
        {
            WebRequest Req = WebRequest.Create(URL);
            Req.Method = "POST";
            Req.ContentType = "application/json";
            if (bToken) { Req.Headers.Add("authorization", Token); }
            Req.GetRequestStream().Write(new byte[] { }, 0, new byte[] { }.Length);
            Req.GetRequestStream().Close();
            WebResponse Res = Req.GetResponse();
            return Newtonsoft.Json.Linq.JObject.Parse(new StreamReader(Res.GetResponseStream()).ReadToEnd());
        }

        public static Newtonsoft.Json.Linq.JObject PostRequest(string URL,string Data,Boolean bToken)
        {
            byte[] bData = Encoding.ASCII.GetBytes(Data);
            WebRequest Req = WebRequest.Create(URL);
            Req.Method = "POST";
            Req.ContentType = "application/json";
            if (bToken) { Req.Headers.Add("authorization", Token); }
            Req.GetRequestStream().Write(bData, 0, bData.Length);
            Req.GetRequestStream().Close();
            WebResponse Res = Req.GetResponse();
            return Newtonsoft.Json.Linq.JObject.Parse(new StreamReader(Res.GetResponseStream()).ReadToEnd());
        }

    }
}
