using shop.Models;

namespace shop.Interfaces
{
    public interface ICartRepository : IRepository
    {
        Task<Cart?> GetCartByClientId(string clientId);

        Task<Game?> GetGameById(int gameId);
    }
}
