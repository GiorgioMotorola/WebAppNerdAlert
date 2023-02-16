using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using WebAppNerdAlert.Helpers;
using WebAppNerdAlert.Interfaces;
using WebAppNerdAlert.Models;
using WebAppNerdAlert.ViewModels;

namespace WebAppNerdAlert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHobbyRepository _hobbyRepository;

        public HomeController(ILogger<HomeController> logger, IHobbyRepository hobbyRepository)
        {
            _logger = logger;
            _hobbyRepository = hobbyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();

            try
            {
                string url = "https://ipinfo.io?token=d9de3392ba40a6";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRegionInfo = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRegionInfo.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Hobbies = await _hobbyRepository.GetHobbyByCity(homeViewModel.City);
                }
                else
                {
                    homeViewModel.Hobbies = null;
                }
                return View(homeViewModel);
            }
            catch
            {
                homeViewModel.Hobbies = null;
            }
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}