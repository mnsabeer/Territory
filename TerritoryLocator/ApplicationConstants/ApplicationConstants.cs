namespace TerritoryLocator
{
    public static class ApplicationConstants
    {

        public static string LogonUrl = "/TerritoryHome";

        public static string AuthenticationTicket = "TerritoryLocatorAuthenticationTicket";

        /// <summary>
        /// the private variable to hold the logged on user cookie name.
        /// </summary>
        public static string LoggedOnUserCookieName = "TerritoryLocatorLoggedOnUserCookie";

        /// <summary>
        /// The private variable to hold the cookie name of the application info.
        /// </summary>
        public static string ApplicationCookieName = "TerritoryLocatorApplicationCookie";

        /// <summary>
        /// The string used to store the provider name .
        /// </summary>
        public const string ProviderName = "System.Data.SqlClient";

    }
}