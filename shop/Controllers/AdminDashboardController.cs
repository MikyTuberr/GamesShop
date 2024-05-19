using Microsoft.AspNetCore.Mvc;
using shop.Interfaces;
using shop.Models;
using shop.ViewModels;

namespace shop.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly IAdminDashboardRepository _dashboardRepository;

        public AdminDashboardController(IAdminDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            var game = await _dashboardRepository.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }

            var viewModel = new EditGameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Genre = game.Genre,
                IsOnSale = game.IsOnSale,
                Price = game.Price,
                PriceAfterSale = game.PriceAfterSale,
                ImagePath = game.ImagePath,
                Alt = game.Alt
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(EditGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var game = await _dashboardRepository.GetGameById(model.Id);
            if (game == null)
            {
                return NotFound();
            }

            game.Name = model.Name;
            game.Description = model.Description;
            game.Genre = model.Genre;
            game.IsOnSale = model.IsOnSale;
            game.Price = model.Price;
            game.PriceAfterSale = model.PriceAfterSale;
            game.ImagePath = model.ImagePath;
            game.Alt = model.Alt;

            _dashboardRepository.Update(game);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(EditGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var game = new Game
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Genre = model.Genre,
                IsOnSale = model.IsOnSale,
                Price = model.Price,
                PriceAfterSale = model.PriceAfterSale,
                ImagePath = model.ImagePath,
                Alt = model.Alt
            };

            _dashboardRepository.Add(game);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id, string path)
        {
            var game = await _dashboardRepository.GetGameById(id);
            _dashboardRepository.Delete(game);
            return RedirectToAction("Index", "Home");
        }
    }
}
