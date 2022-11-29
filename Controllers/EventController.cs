using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;
using WebAppNerdAlert.Repository;
using WebAppNerdAlert.ViewModels;

namespace WebAppNerdAlert.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventController(IEventRepository eventRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _eventRepository = eventRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
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
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createEventViewModel = new CreateEventViewModel { AppUserId = currentUserId };
            return View(createEventViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateEventViewModel eventsVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(eventsVM.Image);

                var events = new Event
                {
                    Title = eventsVM.Title,
                    Description = eventsVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = eventsVM.AppUserId,
                    Address = new Address
                    {
                        Street = eventsVM.Address.Street,
                        City = eventsVM.Address.City,
                        State = eventsVM.Address.State
                    }
                };

                _eventRepository.Add(events);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(eventsVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var events = await _eventRepository.GetByIdAsync(id);
            if (events == null) return View("Error");
            var eventsVM = new EditEventViewModel
            {
                Title = events.Title,
                Description = events.Description,
                AddressId = events.AddressId,
                Address = events.Address,
                Url = events.Image,
                EventCategory = events.EventCategory
            };
            return View(eventsVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel eventsVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed To Edit Event");
                return View("Edit", eventsVM);
            }
            var userEvent = await _eventRepository.GetByIdAsyncNoTracking(id);

            if (userEvent != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userEvent.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could Not Delete Photo");
                    return View(eventsVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(eventsVM.Image);

                var events = new Event
                {
                    Id = id,
                    Title = eventsVM.Title,
                    Description = eventsVM.Description,
                    Image = photoResult.Url.ToString(),
                    Address = eventsVM.Address,
                    AddressId = eventsVM.AddressId
                    
                };

                _eventRepository.Update(events);
                return RedirectToAction("Index");
            }
            else
            {
                return View(eventsVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var eventsDetails = await _eventRepository.GetByIdAsync(id);
            if (eventsDetails == null) return View("Error");
            return View(eventsDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventsDetails = await _eventRepository.GetByIdAsync(id);
            if (eventsDetails == null) return View("Error");

            _eventRepository.Delete(eventsDetails);
            return RedirectToAction("Index");
        }
    }
}
