using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TerritoryLocator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = "(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{resource}.sso/{*pathInfo}");

            routes.MapRoute(
              "AdminHome",
              "AdminHome",
              new { controller = "Home", action = "Admin", id = UrlParameter.Optional });

            routes.MapRoute(
                "RiskMeter", 
                "RiskMeter", 
                new { Controller = "Home", action = "RiskMeter", id = UrlParameter.Optional });

            routes.MapRoute(
               "Home",
               "TerritoryHome",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            //routes.MapRoute(
            //   "Sample",
            //   "Sample",
            //   new { controller = "Home", action = "Sample", id = UrlParameter.Optional });

            routes.MapRoute(
                           "Sample",
                           "Sample",
                           new { controller = "Home", action = "Sampledemo", id = UrlParameter.Optional });

            routes.MapRoute(
               "SubscribeAI",
               "SubscribeAI",
               new { controller = "Home", action = "SubscribeAI", id = UrlParameter.Optional });


            routes.MapRoute(
              "Error",
              "Error",
              new { controller = "Home", action = "Error", id = UrlParameter.Optional });

            routes.MapRoute(
             "Join",
             "Join",
             new { controller = "Home", action = "Subscribe", id = UrlParameter.Optional });
            routes.MapRoute(
             "Renew",
             "Renew",
             new { controller = "Home", action = "Subscribe", id = UrlParameter.Optional });
            routes.MapRoute(
            "Features",
            "Features",
            new { controller = "Home", action = "Features", id = UrlParameter.Optional });

            routes.MapRoute(
            "Transition",
            "Transition",
            new { controller = "Home", action = "Transition", id = UrlParameter.Optional });

            routes.MapRoute(
            "Index",
            "Index",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
            "LogIn",
            "LogIn",
            new { controller = "Home", action = "Login", id = UrlParameter.Optional });


            routes.MapRoute(
               "LogOn",
               "LogOn",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
              "Group",
              "Group",
              new { controller = "Home", action = "Group", id = UrlParameter.Optional });

            routes.MapRoute(
              "ContactUs",
              "ContactUs",
              new { controller = "Home", action = "ContactUs", id = UrlParameter.Optional });
            routes.MapRoute(
                "Pictometry",
                "Pictometry",
                new { Controller = "Home", action = "Pictometry", id = UrlParameter.Optional });
            routes.MapRoute(
                "RiskMeterRedirection",
                "RiskMeterRedirection",
                new { Controller = "Home", action = "RiskMeterRedirection", id = UrlParameter.Optional });
            routes.MapRoute(
                "DataBaseUSA",
                "DataBaseUSA",
                new { Controller = "Home", action = "DataBaseUSA", id = UrlParameter.Optional });
            routes.MapRoute(
               "DevelopmentDocument",
               "DevelopmentDocument",
               new { Controller = "Home", action = "DevelopmentDocument", id = UrlParameter.Optional });

            routes.MapRoute(
             "DownLoadPdf",
             "DownLoadPdf",
             new { Controller = "Home", action = "DownLoadPdf", id = UrlParameter.Optional });
            
            routes.MapRoute(
             "DownLoadListViewExcel",
             "DownLoadListViewExcel",
             new { Controller = "Home", action = "DownLoadListViewExcel", id = UrlParameter.Optional });

            routes.MapRoute(
                    "Development",
                    "Development/Admin",
                    new { Controller = "Home", action = "Development", id = UrlParameter.Optional });

            routes.MapRoute(
          "DownloadBulkUserPasswordResetSampleExcel",
          "DownloadBulkUserPasswordResetSampleExcel",
          new { Controller = "Home", action = "DownloadBulkUserPasswordResetSampleExcel", id = UrlParameter.Optional });

            routes.MapRoute(
          "ExportCustomer",
          "ExportCustomer",
          new { Controller = "Home", action = "ExportCustomer", id = UrlParameter.Optional });

            routes.MapRoute(
     "ErrorLog",
     "ErrorLog",
     new { Controller = "Home", action = "ErrorLog", id = UrlParameter.Optional });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            
        }

    }
}