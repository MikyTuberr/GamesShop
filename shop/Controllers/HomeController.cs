using Microsoft.AspNetCore.Mvc;
using shop.data;
using shop.Interfaces;
using shop.Models;
using shop.ViewModels;
using System.Diagnostics;

namespace shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        private readonly ICookieService _cookieService;

        public HomeController(IHomeRepository homeRepository, ICookieService homeService)
        {
            _homeRepository = homeRepository;
            _cookieService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            if (!_cookieService.CookieExists("clientId"))
            {
                _cookieService.SetCookie("cartItemCount", "0");
                var clientId = Guid.NewGuid().ToString();
                _cookieService.SetCookie("clientId", clientId);
            }

            var games = await _homeRepository.GetAll();
            var viewModel = new HomeIndexViewModel 
            { 
                Games = games 
            };

            return View(viewModel);
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            var game = await _homeRepository.GetByIdAsync(id);
            var games = await _homeRepository.GetAll();

            var viewModel = new HomeDetailsViewModel
            {
                Game = game,
                Games = games 
            };

            return View(viewModel);
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
