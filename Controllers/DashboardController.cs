using Microsoft.AspNetCore.Mvc;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.ViewModels;

namespace WebAppNerdAlert.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userEvents = await _dashboardRepository.GetAllUserEvents();
            var userHobbies = await _dashboardRepository.GetAllUserHobbies();
            var dashboardViewModel = new DashboardViewModel()
            {
                Hobbies = userHobbies,
                Events = userEvents
            };
            return View(dashboardViewModel);
        }
    }
}
