using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using postmark_inbound_demo.API;
using Microsoft.ApplicationServer.Http.Activation;
using System.ServiceModel.Activation;
using Microsoft.ApplicationServer.Http;

namespace postmark_inbound_demo
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

            routes.SetDefaultHttpConfiguration(new WebApiConfiguration() { EnableHelpPage = true, EnableTestClient = true });

            var config = new HttpConfiguration() { EnableTestClient = true };
            routes.Add(new ServiceRoute("api/emails", new HttpServiceHostFactory() { Configuration = config }, typeof(PostmarkApi)));

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