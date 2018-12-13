using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogApp.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Contact",
                url: "iletisim",
                defaults: new { controller = "Home", action = "Contact" },
                namespaces: new[] { "BlogApp.UI.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "hakkimda",
                defaults: new { controller = "Home", action = "About" },
                namespaces: new[] { "BlogApp.UI.Controllers" }
            );

            routes.MapRoute(
                name: "Home",
                url: "anasayfa",
                defaults: new { controller = "Home", action = "Index", categoryId = UrlParameter.Optional },
                namespaces: new[] { "BlogApp.UI.Controllers" }
            );

            //routes.MapRoute(
            //    name: "Home",
            //    url: "Anasayfa/{categoryId}",
            //    defaults: new { controller = "Home", action = "Index", categoryId = UrlParameter.Optional },
            //    namespaces: new[] { "BlogApp.UI.Controllers" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BlogApp.UI.Controllers" }
            );
        }
    }
}
