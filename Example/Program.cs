using System;
using DiscordUserAPI;

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
            var t = await Auth.Login("trialaccount77@gmail.com", "Tr1alAcc0unt");
        }
    }
}
