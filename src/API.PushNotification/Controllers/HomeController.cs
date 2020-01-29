using System.Web.Mvc;

namespace API.PushNotification.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/swagger/ui/index");
        }
    }
}
