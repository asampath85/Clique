using System.Web;
using System.Web.Optimization;

namespace HomeAwayWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/js")
                         .Include("~/Scripts/jquery-{version}.js")
                         .Include("~/Scripts/knockout-3.3.0.js")
                         .Include("~/Scripts/bootstrap.js")
                         );

            bundles.Add(new StyleBundle("~/css")
                           .Include("~/Content/bootstrap.min.css")
                           .Include("~/Content/bootstrap-theme.css")
                           );

        }
    }
}