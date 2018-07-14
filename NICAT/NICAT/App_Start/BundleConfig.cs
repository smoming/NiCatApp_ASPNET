﻿using System.Web;
using System.Web.Optimization;

namespace NICAT
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                        "~/Scripts/umd/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap3-typeahead.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-responsive.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/Site.css",
                      "~/Content/loader_cat.css"));

            //js ext
            bundles.Add(new ScriptBundle("~/bundles/extjs").Include(
                        "~/Scripts/bootbox.min.js"));

            //js customize
            bundles.Add(new ScriptBundle("~/bundles/customizejs").Include(
                        "~/Scripts/CustomizeJS.js"));
        }
    }
}
