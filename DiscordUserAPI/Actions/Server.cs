using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordUserAPI.Actions
{
    public static class Server
    {
        public class Guild
        {
            public string id, name, splash, banner, description, icon, vanity_url_code;
            public int verification_level, nsfw_level;
            public bool nsfw;
        }

        public class Channel
        {
            public string id, name;
            public int type;
        }

        public class User
        {
            public string id, username, avatar, discriminator;
            public int public_flags;
        }

        public class InviteResponse
        {
            public string code;
            public bool new_member;
            public Guild guild;
            public Channel channel;
            public User inviter;
        }

        public static async Task<InviteResponse> JoinUsingInvite(string inviteCode, Auth.AuthResponse auth)
        {
            return await Requests.DoRequest<InviteResponse>($"invites/{inviteCode}", "", authorization: auth.token);
        }
    }
}
