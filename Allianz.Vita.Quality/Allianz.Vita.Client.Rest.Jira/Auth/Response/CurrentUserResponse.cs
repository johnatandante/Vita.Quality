using Allianz.Vita.Client.Rest.Jira.DataModel;
namespace Allianz.Vita.Client.Rest.Jira.Auth.Response
{
    /// <summary>
    /// Returns information about the currently authenticated user's session. If the caller is not authenticated they will get a 401 Unauthorized status code.
    ///{
    ///    "self": "http://www.example.com/jira/rest/api/2.0/user/fred",
    ///    "name": "fred",
    ///    "loginInfo": {
    ///        "failedLoginCount": 10,
    ///        "loginCount": 127,
    ///        "lastFailedLoginTime": "2017-12-07T09:23:17.771+0000",
    ///        "previousLoginTime": "2017-12-07T09:23:17.771+0000"
    ///    }
    ///} 
    /// </summary>
    public class CurrentUserResponse : ResponseField
    {
        public LoginResponse.LoginInfo loginInfo = new LoginResponse.LoginInfo();        
    }
}
