using System;

namespace Allianz.Vita.Client.Rest.Jira.Auth.Response
{
    /// <summary>
    ///{
    ///"session": {
    ///    "name": "JSESSIONID",
    ///    "value": "12345678901234567890"
    ///},
    ///"loginInfo": {
    ///    "failedLoginCount": 10,
    ///    "loginCount": 127,
    ///    "lastFailedLoginTime": "2017-12-07T09:23:17.771+0000",
    ///    "previousLoginTime": "2017-12-07T09:23:17.771+0000"
    ///}
    ///}
    /// </summary>
    public class LoginResponse
    {
        public SessionInfo session = new SessionInfo();

        public LoginInfo loginInfo = new LoginInfo();


        public class SessionInfo {
            public string name = string.Empty;
            public string value = string.Empty;
        }

        public class LoginInfo {
            public int failedLoginCount = 0;
            public int loginCount = 0;
            public DateTime lastFailedLoginTime = DateTime.MinValue;
            public DateTime previousLoginTime = DateTime.MinValue;
        }

    }
}
