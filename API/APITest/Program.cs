using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace APITest
{
    class Program
    {
        static List<Thread> Ts = new List<Thread> { };
        static void Main(string[] args)
        {
            DiscordAPI.Events.Start();
            string Guild = DiscordAPI.Events.JoinServer("tdjt9B");
            Newtonsoft.Json.Linq.JArray GuildChannels = DiscordAPI.Events.GetTextChannels(Guild);
            Console.WriteLine(GuildChannels);
            foreach (Newtonsoft.Json.Linq.JObject TChannel in GuildChannels)
            {
                try
                {
                    DiscordAPI.Events.SendMessage((string)TChannel["id"], "This is a test.\\nHello");
                    break;
                }
                catch { }
            }
            DiscordAPI.Events.LeaveServer(Guild);
            //Console.WriteLine(DiscordAPI.Events.CreateInvite(Channel));
            //Ts.Add(new Thread(()=>DiscordAPI.DiscordInterface.Listen(Channel,Handler)));
            //Ts[Ts.Count-1].Start();
            Console.ReadLine();
        }
        public static void Handler(string Message,string SenderID,string ChannelID)
        {
            if (SenderID != DiscordAPI.Events.MYID)
            {
                Console.WriteLine(Message + " " + SenderID + " " + ChannelID);
                DiscordAPI.Events.SendMessage(ChannelID, Message);
            }
        }
    }
}
