using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.data;
using shop.Data;
using shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string clientId = Request.Cookies["clientId"];
            Cart cart = _context.Carts.Include(c => c.GamesAndQuantities).ThenInclude(gq => gq.Game).FirstOrDefault(c => c.ClientId == clientId);
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(cart.GamesAndQuantities);
        }

        public IActionResult AddToCartWithQuantity(int gameId, int quantity, string path)
        {
            string clientId = Request.Cookies["clientId"];
            Cart cart = _context.Carts.Include(c => c.GamesAndQuantities).ThenInclude(gq => gq.Game).FirstOrDefault(c => c.ClientId == clientId);
            if (cart == null)
            {
                cart = new Cart { ClientId = clientId, GamesAndQuantities = new List<GameAndQuantity>() };
                _context.Carts.Add(cart);
            }

            var existingItem = cart.GamesAndQuantities.FirstOrDefault(gq => gq.Game.Id == gameId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Game game = _context.Games.FirstOrDefault(g => g.Id == gameId);
                if (game != null)
                {
                    cart.GamesAndQuantities.Add(new GameAndQuantity { Game = game, Quantity = quantity });
                }
            }

            UpdateCartItemCountCookie(cart);
            _context.SaveChanges();

            return Route(path, gameId);
        }

        private void UpdateCartItemCountCookie(Cart cart)
        {
            int itemCount = cart.GamesAndQuantities.Sum(item => item.Quantity);
            Response.Cookies.Append("cartItemCount", itemCount.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddDays(7), SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None, Secure = true });
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

        public IActionResult GetCartItemCount()
        {
            string cartItemCountString = Request.Cookies["cartItemCount"];
            int cartItemCount = !string.IsNullOrEmpty(cartItemCountString) ? int.Parse(cartItemCountString) : 0;
            return Json(new { count = cartItemCount });
        }

        public IActionResult DecreaseQuantityOfItemInCart(int gameId)
        {
            string clientId = Request.Cookies["clientId"];
            Cart cart = _context.Carts.Include(c => c.GamesAndQuantities).ThenInclude(gq => gq.Game).FirstOrDefault(c => c.ClientId == clientId);
            if (cart != null)
            {
                var existingItem = cart.GamesAndQuantities.FirstOrDefault(gq => gq.Game.Id == gameId);
                if (existingItem != null)
                {
                    if (existingItem.Quantity > 1)
                    {
                        existingItem.Quantity--; 
                    }
                    else
                    {
                        cart.GamesAndQuantities.Remove(existingItem);
                        if (cart.GamesAndQuantities.Count() == 0)
                        {
                            _context.Remove(cart);
                        }
                    }

                    UpdateCartItemCountCookie(cart);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult RemoveItemFromCart(int gameId)
        {
            string clientId = Request.Cookies["clientId"];
            Cart cart = _context.Carts.Include(c => c.GamesAndQuantities).ThenInclude(gq => gq.Game).FirstOrDefault(c => c.ClientId == clientId);

            if (cart != null)
            {
                var existingItem = cart.GamesAndQuantities.FirstOrDefault(gq => gq.Game.Id == gameId);

                if (existingItem != null)
                {
                    cart.GamesAndQuantities.Remove(existingItem);

                    if (cart.GamesAndQuantities.Count == 0)
                    {
                        _context.Carts.Remove(cart);
                        _context.SaveChanges();
                        Response.Cookies.Delete("cartItemCount"); 
                        return RedirectToAction("Index", "Home");
                    }

                    UpdateCartItemCountCookie(cart);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Cart");
        }

    }
}
