using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;


namespace Hazza.Dashboard
{
    public class Routes : IRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor
                {
                    Route = new Route("Admin/Dashboard/{action}",
                        new RouteValueDictionary
                        {
                            {"area", "Hazza.Dashboard"},
                            {"controller", "Admin"},
                            {"action", "{action}"}
                        }, new RouteValueDictionary(),
                        new RouteValueDictionary {{"area", "Hazza.Dashboard"}},
                        new MvcRouteHandler())
                },
                new RouteDescriptor
                {
                    Priority = 20,
                    Route = new Route("Admin",
                        new RouteValueDictionary
                        {
                            {"area", "Hazza.Dashboard"},
                            {"controller", "Admin"},
                            {"action", "Index"}
                        }, new RouteValueDictionary(),
                        new RouteValueDictionary {{"area", "Hazza.Dashboard"}},
                        new MvcRouteHandler())
                } 
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (RouteDescriptor route in GetRoutes())
            {
                routes.Add(route);
            }
        }
    }
}