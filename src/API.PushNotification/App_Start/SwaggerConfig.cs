using System.Web.Http;
using WebActivatorEx;
using API.PushNotification;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace API.PushNotification
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "API.PushNotification");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\API.PushNotification.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
