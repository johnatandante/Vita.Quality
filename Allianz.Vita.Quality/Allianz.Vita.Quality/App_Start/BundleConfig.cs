using System.Web;
using System.Web.Optimization;

namespace Allianz.Vita.Quality
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/jQuery.flashMessage.js"
                        ));

			bundles.Add(new ScriptBundle("~/bundles/react").Include(
				// Add your JSX files here
				"~/Scripts/HelloWorld.jsx" //,
							//"~/Scripts/AnythingElse.jsx",
							// You can include regular JavaScript files in the bundle too
							//"~/Scripts/ajax.js",
				));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-bundle.js",
                      "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
                      "~/Content/bootstrap-grid.css",
                      "~/Content/site.css"));



		}
	}
}
