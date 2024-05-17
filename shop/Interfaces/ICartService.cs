using Microsoft.AspNetCore.Mvc;
using shop.Models;

namespace shop.Interfaces
{
    public interface ICartService
    {
        void UpdateCartItemCountCookie(Cart cart);

        GameAndQuantity? GetGameAndQuantityById(Cart cart, int gameId);

        void AddGameAndQuantityToCart(GameAndQuantity gameAndQuantity, Cart cart);
    }
}
