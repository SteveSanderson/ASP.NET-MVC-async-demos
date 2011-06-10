using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FakeWebServices
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "scottgu/rss.aspx", new { controller = "FakeWebService", action = "FetchData", preset = "http://weblogs.asp.net/scottgu/rss.aspx", contentType = "application/xml" });
            routes.MapRoute(null, "presspass/rss/RSSFeed.aspx", new { controller = "FakeWebService", action = "FetchData", preset = "http://www.microsoft.co.uk/presspass/rss/RSSFeed.aspx", contentType = "application/xml" });
            routes.MapRoute(null, "status/user_timeline/billgates.json", new { controller = "FakeWebService", action = "FetchData", preset = "http://twitter.com/status/user_timeline/billgates.json?count=10", contentType = "application/json" });
            routes.MapRoute(null, "status/user_timeline/bbcnews.json", new { controller = "FakeWebService", action = "FetchData", preset = "http://twitter.com/status/user_timeline/bbcnews.json?count=6", contentType = "application/json" });
        
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}