using System.Web.Http;
using System.Web.Http.Cors;

namespace API.PushNotification.Controllers
{
    [System.Web.Http.RoutePrefix("api")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        
	}
}