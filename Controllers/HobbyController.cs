using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;
using WebAppNerdAlert.ViewModels;

namespace WebAppNerdAlert.Controllers
{
    public class HobbyController : Controller
    {
        
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HobbyController(IHobbyRepository hobbyRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _hobbyRepository = hobbyRepository;
            _photoService = photoService;
        }

        public async Task <IActionResult> Index()
        {
            IEnumerable<Hobby> hobbies = await  _hobbyRepository.GetAll();    
            return View(hobbies);
        }


        public async Task <IActionResult> Detail(int id)
        {
            Hobby hobbies = await _hobbyRepository.GetByIdAsync(id);
            return View(hobbies);
        }
        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createHobbyViewModel = new CreateHobbyViewModel { AppUserId = currentUserId };
            return View(createHobbyViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateHobbyViewModel hobbiesVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(hobbiesVM.Image);

                var hobbies = new Hobby
                {
                    Title = hobbiesVM.Title,
                    Description = hobbiesVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = hobbiesVM.AppUserId,
                    Address = new Address
                    { 
                        Street = hobbiesVM.Address.Street,
                        City = hobbiesVM.Address.City,
                        State = hobbiesVM.Address.State
                    }
                };

                    _hobbyRepository.Add(hobbies);
                    return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(hobbiesVM);
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hobbies = await _hobbyRepository.GetByIdAsync(id);
            if (hobbies == null) return View("Error");
            var hobbiesVM = new EditHobbyViewModel
            {
                Title = hobbies.Title,
                Description = hobbies.Description,
                AddressId = hobbies.AddressId,
                Address = hobbies.Address,
                Url = hobbies.Image,
                HobbyCategory = hobbies.HobbyCategory
            };
            return View(hobbiesVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditHobbyViewModel hobbiesVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed To Edit Hobby");
                return View("Edit", hobbiesVM);
            }

            var userHobby = await _hobbyRepository.GetByIdAsyncNoTracking(id);

            if (userHobby != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userHobby.Image);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could Not Delete Photo");
                    return View(hobbiesVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(hobbiesVM.Image);

                var hobbies = new Hobby
                {
                    Id = id,
                    Title = hobbiesVM.Title,
                    Description = hobbiesVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = hobbiesVM.AddressId,
                    Address = hobbiesVM.Address
                };

                _hobbyRepository.Update(hobbies);
                return RedirectToAction("Index");
            }
            else
            {
                return View(hobbiesVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hobbiesDetails = await _hobbyRepository.GetByIdAsync(id);
            if (hobbiesDetails == null) return View("Error");
            return View(hobbiesDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteHobby(int id)
        {
            var hobbyDetails = await _hobbyRepository.GetByIdAsync(id);
            if (hobbyDetails == null) return View("Error");

            _hobbyRepository.Delete(hobbyDetails);
            return RedirectToAction("Index");
        }
    }
}
