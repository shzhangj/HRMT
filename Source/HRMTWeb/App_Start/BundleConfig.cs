using System.Web;
using System.Web.Optimization;

namespace HRMTWeb
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/jquery.mobile/js/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.mobile").Include(
                        "~/Content/jquery.mobile/js/jquery.mobile-1.4.5.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/mobiscroll").Include(
                        "~/Content/mobiscroll/js/mobiscroll.custom-2.5.0.js"));

            bundles.Add(new StyleBundle("~/bundles/mobiscroll/css").Include(
                "~/Content/mobiscroll/css/mobiscroll.custom-2.5.0.css"));

            /*
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
                        
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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
            

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/Bootstrap/bootstrap/css/*.css",
                "~/Content/Bootstrap/default.css"));
            */
            bundles.Add(new StyleBundle("~/bundles/jquery.mobile/css").Include(
                "~/Content/jquery.mobile/css/*.css",
                "~/Content/jquery.mobile/default.css"));
        }
    }
}