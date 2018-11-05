using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Colin_Biblio
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");                     

            routes.MapRoute
            (
                name: "Auteur",
                url: "Afficher/Auteur/{IDauteur}",
                new { controller = "Afficher", action = "Auteur", IDauteur = 0 }
            );

            routes.MapRoute
            (
                name: "Livre",
                url: "Afficher/Livre/{IDlivre}",
                new { controller = "Afficher", action = "Livre", IDlivre = 0 }
            );

            routes.MapRoute
            (
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
