using Microsoft.AspNetCore.Mvc;
using shop.Interfaces;
using shop.Models;

namespace shop.Services
{
    public class CartService : ICartService
    {
        private readonly ICookieService _cookieService;

        public CartService(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public void AddGameAndQuantityToCart(GameAndQuantity gameAndQuantity, Cart cart)
        {
            cart.GamesAndQuantities.Add(gameAndQuantity);
        }

        public GameAndQuantity? GetGameAndQuantityById(Cart cart, int gameId)
        {
            return cart.GamesAndQuantities.FirstOrDefault(gq => gq.Game.Id == gameId);
        }

        public void UpdateCartItemCountCookie(Cart cart)
        {
            int itemCount = cart.GamesAndQuantities.Sum(item => item.Quantity);
            _cookieService.SetCookie("cartItemCount", itemCount.ToString());
        }
    }
}
