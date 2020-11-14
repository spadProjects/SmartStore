using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                              name: "GroupProductList",
                              url: "ProductList/Group/{groupid}/{Page}",
                              defaults: new { controller = "Shop", action = "GroupProductList", page = UrlParameter.Optional }
                          );
            routes.MapRoute(
                name: "SubGroupProductList",
                url: "ProductList/SubGroup/{title}/{subgroupid}/{Page}",
                defaults: new { controller = "Shop", action = "SubGroupProductList", title = UrlParameter.Optional, page = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ProductDetails",
                url: "ProductDetails/{id}/{title}",
                defaults: new { controller = "Shop", action = "ProductDetails", title = UrlParameter.Optional }

            );
            routes.MapRoute(
    name: "BlogList",
    url: "BlogList/{page}",
    defaults: new { controller = "Home", action = "Blog", page = UrlParameter.Optional }

);
            routes.MapRoute(
                name: "SubGroupsPanel",
                url: "Admin/SubGroups/Index/{title}/{id}",
                defaults: new { controller = "SubGroups", action = "Index", title = UrlParameter.Optional }

            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
