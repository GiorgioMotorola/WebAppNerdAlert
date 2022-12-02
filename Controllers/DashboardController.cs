using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;
using WebAppNerdAlert.ViewModels;

namespace WebAppNerdAlert.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _photoService = photoService;
            _httpcontextAccessor = httpContextAccessor;
            _dashboardRepository = dashboardRepository;
        }
        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVm, ImageUploadResult photoResult)
        {
            user.Id = editVm.Id;
            user.City = editVm.City;    
            user.State = editVm.State;
            user.ProfileImageUrl = photoResult.Url.ToString();
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

        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpcontextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(currentUserId);
            if (user == null) return View("Error");
            var editUserDashboardViewModel = new EditUserDashboardViewModel()
            {
               Id = currentUserId,
               ProfileImageUrl = user.ProfileImageUrl,
               City = user.City,
               State = user.State
            };
            return View(editUserDashboardViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit Profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if(user.ProfileImageUrl == "" || user.ProfileImageUrl == null) 
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could Not Delete Photo");
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
