using System.Web;
using System.Web.Optimization;

namespace UserProfilerWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/js")
                         .Include("~/Scripts/jquery-{version}.js")                         
                         .Include("~/Scripts/knockout-3.3.0.js")
                         .Include("~/Scripts/bootstrap.js")
                         //.Include("~/Scripts/cogShift.knockout.extensions.js")
                         );

            bundles.Add(new StyleBundle("~/css")
                           .Include("~/Content/bootstrap.css")
                           .Include("~/Content/bootstrap-theme.css")
                //.Include("~/Content/site.css")
                           );


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
