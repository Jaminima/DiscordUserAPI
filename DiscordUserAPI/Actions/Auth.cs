using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DiscordUserAPI.Actions
{
    public static class Auth
    {
        public class LoginRequest
        {
            public string login, password, captcha_key=null, login_source=null, gift_code_sku_id=null;
            public bool undelete = false;

            public LoginRequest(string username, string password)
            {
                this.login = username;
                this.password = password;
            }
        }

        public class AuthResponse
        {
            public string token;
        }

        public static async Task<AuthResponse> Login(string username, string password)
        {
            return await Requests.DoRequest<AuthResponse>("auth/login", new LoginRequest(username,password));
        }
    }
}
