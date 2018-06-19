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
            string Channel = DiscordAPI.Events.JoinServer("AcdPMe");
            //DiscordAPI.Events.SendMessage(Channel,"Im Alive");
            Console.WriteLine(DiscordAPI.Events.CreateInvite(Channel));
            Ts.Add(new Thread(()=>DiscordAPI.DiscordInterface.Listen(Channel,Handler)));
            Ts[Ts.Count-1].Start();
            Ts.Add(new Thread(() => DiscordAPI.DiscordInterface.Listen("455485369165807668", Handler)));
            Ts[Ts.Count - 1].Start();
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
