using shop.Models;

namespace shop.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByClientId(string clientId);

        Task<Game?> GetGameById(int gameId);

        bool Add(GameAndQuantity gameAndQuantity);

        bool Add(Cart cart);

        bool Update(Cart cart);
        bool Update(GameAndQuantity gameAndQuantity);

        bool Delete(GameAndQuantity gameAndQuantity);

        bool Delete(Cart cart);

        bool Save();
    }
}
