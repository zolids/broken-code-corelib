using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Content/js").Include("~/Content/theme/css/jquery.min.js",
                        "~/Content/theme/css/bootstrap.min.js",
                        "~/Content/theme/css/detect.js",
                        "~/Content/theme/css/fastclick.js",
                        "~/Content/theme/css/jquery-blockUI.js",
                        "~/Content/theme/css/waves.js",
                        "~/Content/theme/css/wow.min.js",
                        "~/Content/theme/css/jquery.nicescroll.js",
                        "~/Content/theme/css/jquery.scrollTo.min.js",
                        "~/Content/theme/css/jquery.core.js",
                        "~/Content/theme/css/jquery.app.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/theme").Include("~/Content/theme/css/bootstrap.min.css",
                        "~/Content/theme/css/core.css",
                        "~/Content/theme/css/component.css",
                        "~/Content/theme/css/icon.css",
                        "~/Content/theme/css/pages.css",
                        "~/Content/theme/css/menu.css",
                        "~/Content/theme/css/responsive.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/loading-screen.css",
                        "~/Content/plugins/font-awesome/css/font-awesome.min.css",
                        "~/Content/breadcrumb.css",
                        "~/Content/app.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}