using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a UserAPI Instance using the given Email and Password
            DiscordUserAPI.Instance NewInstance = new DiscordUserAPI.Instance("Email","Password");

            //The User will join the guild using the given invite code
            string GuildID = NewInstance.Actions.JoinGuild("INVITECODE");

            //Gets a list of all channel ids inside of the guild
            List<string> ChannelIDs = NewInstance.Fetch.TextChannels(GuildID);

            //Loops through every channel id
            foreach (string ChannelID in ChannelIDs)
            {
                //User sends a message to each channel id in the list
                NewInstance.Actions.SendMessage(ChannelID, "Hello World");
            }
        }
    }
}
