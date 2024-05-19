using Microsoft.AspNetCore.Mvc;
using shop.Interfaces;
using shop.ViewModels;

namespace shop.Controllers
{
    public class CheckoutController : Controller
    {

        private readonly ICookieService _cookieService;
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutController(ICookieService cookieService, ICheckoutRepository checkoutRepository)
        {
            _cookieService = cookieService;
            _checkoutRepository = checkoutRepository;   
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var clientId = _cookieService.GetCookie("clientId");
            if (clientId == null)
            {
                throw new InvalidOperationException("clientId is null");
            }
            var cart = await _checkoutRepository.GetCartByClientId(clientId);

            if (cart == null)
            {
                throw new InvalidOperationException("Cart is null.");
            }

            var viewModel = new CheckoutViewModel
            {
                GamesAndQuantities = cart.GamesAndQuantities
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(CheckoutViewModel model)
        {
            foreach (var gameAndQuantity in model.GamesAndQuantities)
            {
                var game = await _checkoutRepository.GetGameById(gameAndQuantity.Game.Id);

                if (game != null)
                {
                    gameAndQuantity.Game = game;
                }
                else
                {
                    // when not found
                }
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            return RedirectToAction("ThankYou");
        }


    }
}
