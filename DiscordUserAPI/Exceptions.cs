using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordUserAPI
{
    public static class Exceptions
    {
        public static Exception Credentials = new Exception("Failed to sign in, Email or Password may be invalid");
    }
}
