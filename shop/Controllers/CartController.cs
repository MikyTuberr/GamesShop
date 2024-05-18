using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.data;
using shop.Interfaces;
using shop.Models;
using shop.Services;
using shop.ViewModels;

namespace shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartService _cartService;
        private readonly ICookieService _cookieService;

        public CartController(ICartRepository cartRepository, ICartService cartService, ICookieService cookieService)
        {
            _cartRepository = cartRepository;
            _cookieService = cookieService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var clientId = _cookieService.GetCookie("clientId");
            if(clientId == null)
            {
                throw new InvalidOperationException("clientId is null");
            }
            var cart = await _cartRepository.GetCartByClientId(clientId);
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new CartViewModel 
            { 
                GamesAndQuantities= cart.GamesAndQuantities 
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CheckoutAsync()
        {
            var clientId = _cookieService.GetCookie("clientId");
            if (clientId == null)
            {
                throw new InvalidOperationException("clientId is null");
            }
            var cart = await _cartRepository.GetCartByClientId(clientId);

            if (cart == null)
            {
                throw new InvalidOperationException("Cart is null.");
            }

            var viewModel = new CartViewModel
            {
                GamesAndQuantities = cart.GamesAndQuantities
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCartWithQuantityAsync(int gameId, int quantity, string path)
        {
            var clientId = _cookieService.GetCookie("clientId");
            if (clientId == null)
            {
                throw new InvalidOperationException("clientId is null");
            }
            var cart = await _cartRepository.GetCartByClientId(clientId);

            if (cart == null)
            {
                cart = new Cart { ClientId = clientId, GamesAndQuantities = new List<GameAndQuantity>() };
                _cartRepository.Add(cart);
            }

            var existingItem = _cartService.GetGameAndQuantityById(cart, gameId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                _cartRepository.Update(existingItem);
            }
            else
            {
                var game = await _cartRepository.GetGameById(gameId);
                if (game != null)
                {
                   _cartService.AddGameAndQuantityToCart(new GameAndQuantity { Game = game, Quantity = quantity }, cart);
                    _cartRepository.Save();
                }
            }

            _cartService.UpdateCartItemCountCookie(cart);
            return Route(path, gameId);
        }

        public IActionResult Route(string path, int gameId)
        {
            if (path.Contains("Index"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (path.Contains("Details"))
            {
                return RedirectToAction("Details", "Home", new { id = gameId });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> DecreaseQuantityOfItemInCartAsync(int gameId)
        {
            var clientId = _cookieService.GetCookie("clientId");
            if (clientId == null)
            {
                throw new InvalidOperationException("clientId is null");
            }
            var cart = await _cartRepository.GetCartByClientId(clientId);
            if (cart != null)
            {
                var existingItem = _cartService.GetGameAndQuantityById(cart, gameId);
                if (existingItem != null)
                {
                    if (existingItem.Quantity > 1)
                    {
                        existingItem.Quantity--;
                        _cartRepository.Update(existingItem);
                    }
                    else
                    {
                        _cartRepository.Delete(existingItem);
                        if (cart.GamesAndQuantities.Count() == 0)
                        {
                            _cartRepository.Delete(cart);
                        }
                    }

                    _cartService.UpdateCartItemCountCookie(cart);
                }
            }

            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> IncreaseQuantityOfItemInCartAsync(int gameId)
        {
            var clientId = _cookieService.GetCookie("clientId");
            if (clientId == null)
            {
                throw new InvalidOperationException("clientId is null");
            }
            var cart = await _cartRepository.GetCartByClientId(clientId);

            if (cart != null)
            {
                var existingItem = cart.GamesAndQuantities.FirstOrDefault(gq => gq.Game.Id == gameId);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                    _cartRepository.Update(existingItem);
                    _cartService.UpdateCartItemCountCookie(cart);
                }
            }
            return RedirectToAction("Index", "Cart");
        }


        public async Task<IActionResult> RemoveItemFromCartAsync(int gameId)
        {
            var clientId =_cookieService.GetCookie("clientId");
            if (clientId == null)
            {
                throw new InvalidOperationException("clientId is null");
            }
            var cart = await _cartRepository.GetCartByClientId(clientId);

            if (cart != null)
            {
                var existingItem = cart.GamesAndQuantities.FirstOrDefault(gq => gq.Game.Id == gameId);

                if (existingItem != null)
                {
                    cart.GamesAndQuantities.Remove(existingItem);
                    _cartRepository.Update(existingItem);

                    if (cart.GamesAndQuantities.Count == 0)
                    {
                        _cartRepository.Delete(cart);
                        _cookieService.DeleteCookie("cartItemCount"); 
                        return RedirectToAction("Index", "Home");
                    }

                    _cartService.UpdateCartItemCountCookie(cart);
                }
            }

            return RedirectToAction("Index", "Cart");
        }

    }
}
