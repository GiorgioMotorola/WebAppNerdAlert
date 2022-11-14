using Microsoft.AspNetCore.Mvc;

namespace WebAppNerdAlert.Controllers
{
    public class HobbyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
