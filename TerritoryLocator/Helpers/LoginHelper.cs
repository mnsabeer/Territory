using System.Web.Mvc;
using TerritoryLocator.Security.Session;

namespace TerritoryLocator.Helpers
{
    public static class LoginHelper
    {
        public static bool IsLoggedIn(this HtmlHelper htmlHelper)
        {
            var session = (Session)htmlHelper.ViewContext.HttpContext.Session["UserSession"];

            return session != null && session.IsLoggedIn;
        }
    }
}