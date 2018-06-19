using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordAPI.Config.LoadConfig();
            DiscordAPI.DiscordInterface.SignIn();
            string Channel = DiscordAPI.Events.JoinServer("AcdPMe");
            DiscordAPI.Events.SendMessage(Channel,"Yeet");
            Console.WriteLine(DiscordAPI.Events.CreateInvite(Channel));
            Console.ReadLine();
        }
    }
}
