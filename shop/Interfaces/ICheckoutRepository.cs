using Microsoft.AspNetCore.Mvc;
using shop.Models;

namespace shop.Interfaces
{
    public interface ICheckoutRepository : IRepository
    {
        Task<Cart?> GetCartByClientId(string clientId);

        Task<Game?> GetGameById(int gameId);
    }
}
