using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoManager.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("UnhandledExceptions", "Error", new { controller = "Error", action = "Index" });

            routes.MapRoute(
                name: "album-by-title",
                url: "album-by-title/{url}",
                defaults: new { controller = "Photo", action = "PhotoListByUrl" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Album", action = "AlbumList", id = UrlParameter.Optional }
            );
        }
    }
}
