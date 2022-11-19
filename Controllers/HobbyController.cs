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

        public HobbyController(IHobbyRepository hobbyRepository, IPhotoService photoService)
        {
            
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
            return View();
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
    }
}
