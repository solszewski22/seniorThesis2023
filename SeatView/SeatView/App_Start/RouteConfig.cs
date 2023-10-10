using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SeatView
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // a route that connections the controller, called 'Login' when the '.../login' route is searched
            routes.MapRoute(
                name: "Login",
                url: "{Login}/{action}",
                defaults: new { controller = "Login", action = "Index"}
            );

            // a route that connects the controller called 'NewAccount' when the '.../newaccount' route is searched
            routes.MapRoute(
                name: "NewAccount",
                url: "{NewAccount}/{action}",
                defaults: new { controller = "NewAccount", action = "Index" }
            );
        }
    }
}
