using Microsoft.AspNetCore.Mvc;

namespace WebAppNerdAlert.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
