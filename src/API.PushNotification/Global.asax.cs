using System;
using System.Web;
using System.Linq;
using SimpleInjector;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;
using System.Collections.Generic;
using SimpleInjector.Integration.WebApi;

using API.PushNotification.Filters;
using API.PushNotification.AutoMapper;
using Icomon.PushNotification.Infra.CrossCutting.IoC;

namespace API.PushNotification
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Filters.Add(new ExceptionsFilter());

            AutoMapperConfig.RegisterMappings();

            var container = new Container();
            var resolvers = new SimpleInjectorResolver(container);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
