

using System.Web;
using TerritoryLocator.Security.Model;

namespace TerritoryLocator.Security.Session
{
    public class Session
    {
        public Session()
        {
            IsLoggedIn = false;
            SessionData = new SessionData();
        }
        public SessionData SessionData { get; set; }

        public bool IsLoggedIn { get; set; }

        public Session(HttpContextBase context)
        {
            UserData(context);
        }

        public Session UserData(HttpContextBase context)
        {
            var sessionData = new SessionData
            {
                Email = context.Request.ServerVariables["HTTP_email"] ?? string.Empty,
                FirstName = context.Request.ServerVariables["HTTP_givenname"] ?? string.Empty,
                LastName = context.Request.ServerVariables["HTTP_sn"] ?? string.Empty,
                UserId = context.Request.ServerVariables["HTTP_uid"] ?? string.Empty
            };

            IsLoggedIn = true;
            SessionData = sessionData;

            return this;
        }
    }
}
