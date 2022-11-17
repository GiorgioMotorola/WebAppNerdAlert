using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Controllers
{
    public class HobbyController : Controller
    {
        
        private readonly IHobbyRepository _hobbyRepository;

        public HobbyController(IHobbyRepository hobbyRepository)
        {
            
            _hobbyRepository = hobbyRepository;
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
    }
}
