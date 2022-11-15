using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Controllers
{
    public class HobbyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HobbyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Hobby> hobbies = _context.Hobbies.ToList();
            return View(hobbies);
        }


        public IActionResult Detail(int id)
        {
            Hobby hobby = _context.Hobbies.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(hobby);
        }
    }
}
