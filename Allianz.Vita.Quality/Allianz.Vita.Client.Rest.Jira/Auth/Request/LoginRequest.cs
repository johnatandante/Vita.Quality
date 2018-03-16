namespace Allianz.Vita.Client.Rest.Jira.Auth.Request
{
    /// <summary>
    /// {
    ///    "username": "fred",
    ///    "password": "freds_password"
    ///}
    /// </summary>
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
