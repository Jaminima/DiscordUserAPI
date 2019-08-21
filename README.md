# Discord User API
This Library is designed to allow for easy interaction with discord</br>
As if you were a real user account, instead of a bot

This is acheived by replicating the methods used by a normal account</br>
For authentication and interaction
# Example
This simple code will login, join a discord and send a message to every channel inside of the guild

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
For an example of what can be acheived using this library, look at [DiscordWalker](https://github.com/Jaminima/DiscordWalker), </br>
Which will give you an idea of the potential power of this library.
# Library Reference
Here you will find a reference to all functions and methods in the library
## Actions
`string JoinGuild(string InviteCode)`  Will join using the given InviteCode and return the GuildsID

`string JoinGroup(string InviteCode)` Will join the given InviteCode and return the Groups ChannelID

`void LeaveGuild(string GuildID)` Leaves the Guild 

`bool GuildExists(string InviteCode)` Checks to see if the InviteCode corresponds to a valid invite

`string SendMessage(string ChannelID, string Message)` Sends the given message to the given Channel

`string CreateInvite(string ChannelID)` Creates a InviteCode for the given channel
## Fetch
`List<String> TextChannels(string GuildID)` Returns a list of all ChannelIDs in the given guild

`string ServerID(string InviteCode)` Returns the GuildID of the corresponding invite

`List<JToken> Messages(string ChannelID, int Limit=10)` Returns the most recent messages in the given channel, up to the given limit (Default 10)
## NetworkInterface
`Bool SignIn(string Email, string Password)` Will attempt to sign into the given user account

`JToken Request(string URL, string Data="", Bool WithAuthToken=false, string Method="POST")` returns the formatted JSON response from the performed request</br>
`WithAuthToken` Indicates if the  stored auth token should be included in headers</br>
This function will require some experimentation and trial on your part.
