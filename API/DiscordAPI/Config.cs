using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordAPI
{
    public static class Config
    {
        public static Newtonsoft.Json.Linq.JObject CurConfig;
        public static void LoadConfig()
        {
            try { CurConfig = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(System.IO.File.ReadAllText("./BotConfig.json")); }
            catch { CurConfig = (
                    Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.Linq.JObject.Parse("{SignIn:{Email:\"\",Password:\"\"}}");
                    SaveConfig();  }
        }
        public static void SaveConfig()
        {
            System.IO.File.WriteAllText("./BotConfig.json", Newtonsoft.Json.JsonConvert.SerializeObject(CurConfig));
        }
    }
}
