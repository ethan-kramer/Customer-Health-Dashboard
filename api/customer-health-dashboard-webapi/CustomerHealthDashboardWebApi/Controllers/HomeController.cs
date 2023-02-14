using Microsoft.AspNetCore.Mvc;

namespace CustomerHealthDashboardWebApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
