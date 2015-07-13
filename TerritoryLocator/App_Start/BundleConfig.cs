using System.Web;
using System.Web.Optimization;

namespace TerritoryLocator
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/Scripts/Territory_JS")
                                         .IncludeDirectory("~/Scripts", "*.js")
                                         .IncludeDirectory("~/Scripts/Framework", "*.js")
                                         .IncludeDirectory("~/Content/bootstrap/js/", "*.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/Territory_CSS")
                                         .Include("~/Content/jquery-ui.css")
                                         .Include("~/Content/TerritoryMain.css"));                                   
            BundleTable.Bundles.Add(new StyleBundle("~/Content/BootStrap_CSS").Include("~/Content/bootstrap/css/bootstrap.css"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Scripts/IndexController")
                                       .Include("~/Scripts/HomeScriptController/IndexRootController.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Scripts/LayOutController")
                                      .Include("~/Scripts/HomeScriptController/LayOutRootController.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Scripts/GoogleMapController")
                                     .Include("~/Scripts/GoogleMap.js"));
 
        }
    }
}