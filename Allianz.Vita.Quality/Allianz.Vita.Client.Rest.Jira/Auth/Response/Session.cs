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
    public class Session
    {
        public SessionInfo session = new SessionInfo();

        public LoginInfo loginInfo = new LoginInfo();


        public class SessionInfo {
            public string name;
            public string value;
        }

        public class LoginInfo {
            public int failedLoginCount;
            public int loginCount;
            public DateTime lastFailedLoginTime;
            public DateTime previousLoginTime;
        }

    }
}
