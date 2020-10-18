using System.Web;
using System.Web.Optimization;

namespace Bazaar
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/bootstrap4.min.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap4.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
            ));




            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap4.min.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/DataTables/css/dataTables.bootstrap4.css",
                      "~/Content/admin-custom.css",
                      "~/Content/fontawesome-all.css",
                      "~/Content/custom-main-page.css",
                      "~/Content/custom-nav.css",
                      "~/Content/custom-carousel.css",
                      "~/Content/custom-game-page.css",
                      "~/Content/jquery.mb.gallery.min.css",
                      "~/Content/custom-cart.css",
                      "~/Content/custom-faq.css",
                      "~/Content/custom-catalog.css"));


            bundles.Add(new ScriptBundle("~/Content/admin-css").Include(
                    "~/Content/bootstrap4.min.css",
                    "~/Content/site.css",
                    "~/Content/jquery-ui.css",
                    "~/Content/DataTables/css/dataTables.bootstrap4.css",
                    "~/Content/admin-custom.css",
                    "~/Content/fontawesome-all.css",
                    "~/Content/custom-nav.css",
                    "~/Content/custom-carousel.css",
                    "~/Content/custom-new-game.css",
                    "~/Content/custom-admin-page.css"));
                



        }
    }
}
