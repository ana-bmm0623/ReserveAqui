using System.Web.Mvc;
using System.Web.Routing;

namespace ReserveAqui
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

            routes.MapRoute(
                name: "DataDeletion",
                url: "user/data-deletion",
                defaults: new { controller = "User", action = "DataDeletion" }
            );


        }
    }
}
