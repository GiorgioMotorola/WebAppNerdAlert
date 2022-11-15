using Microsoft.AspNetCore.Mvc;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Event> events = _context.Events.ToList();
            return View(events);
        }
    }
}
