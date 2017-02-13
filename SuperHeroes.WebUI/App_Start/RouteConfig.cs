using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuperHeroes.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute
            (
                name: "SuperHeroEdit",
                url: "Superhero/Edit",
                defaults: new { controller = "Superhero", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute
            (
                name: "SuperPowerGetImage",
                url: "Superpower/Create{superpowerId}",
                defaults: new { controller = "Superpower", action = "GetImage" }
            );

            routes.MapRoute
            (
                name: "SuperPowerCreate",
                url: "Superpower/Create",
                defaults: new { controller = "Superpower", action = "Create" }
            );

            routes.MapRoute
            (
                name: "SuperPowerEdit",
                url: "Superpower/Edit",
                defaults: new { controller = "Superpower", action = "Edit", id = UrlParameter.Optional }
            );
            
            routes.MapRoute
            (
                name: "SuperpowerBySuperhero",
                url: "Superpower/{superhero}",
                defaults: new { controller = "Superpower", action = "Index" }
            );

            routes.MapRoute
                (
                    name: "SuperPowerIndex",
                    url: "Superpower",
                    defaults: new { controller = "Superpower", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Superhero", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
