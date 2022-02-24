using System;
using System.Web;
using System.Web.Optimization;

namespace BookMall.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css")
                .Include("~/Content/bootswatch/minty/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                .Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-3.0.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                       "~/Scripts/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                       "~/Scripts/main.js"));
        }
    }
}