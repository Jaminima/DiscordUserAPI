using System;
using DiscordUserAPI.Actions;
using System.Threading;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            App();

            Console.ReadLine();
        }

        static async void App()
        {
            var t = await Auth.Login("trialaccount79@gmail.com", "Tr1alAcc0unt78");
            Thread.Sleep(5000);
            var i = await Server.JoinUsingInvite("8EKnGgWx", t);
        }
    }
}
