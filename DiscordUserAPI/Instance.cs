using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordUserAPI
{
    public class Instance
    {
        Discord.NetworkInterface NetworkInterface = new Discord.NetworkInterface();
        public Discord.Actions Actions;
        public Discord.Fetch Fetch;

        public Instance(string Email, string Password)
        {
            if (!NetworkInterface.SignIn(Email, Password)) { throw Exceptions.Credentials; }
            Actions = new Discord.Actions(this.NetworkInterface);
            Fetch = new Discord.Fetch(this.NetworkInterface);
        }
    }
}
