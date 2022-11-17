using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;
using WebAppNerdAlert.Repository;

namespace WebAppNerdAlert.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Event> events = await _eventRepository.GetAll();
            return View(events);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Event events = await _eventRepository.GetByIdAsync(id);
            return View(events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Event events)
        {
            if (!ModelState.IsValid)
            {
                return View(events);
            }
            _eventRepository.Add(events);
            return RedirectToAction("Index");
        }
    }
}
