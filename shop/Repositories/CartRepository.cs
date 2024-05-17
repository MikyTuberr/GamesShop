using Microsoft.EntityFrameworkCore;
using Nobisoft.Core.SnowflakeId;
using shop.data;
using shop.Interfaces;
using shop.Models;

namespace shop.Repositories
{
    public class CartRepository : ICartRepository
    {
        ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Cart cart)
        {
            _context.Add(cart);
            return Save();
        }

        public bool Add(GameAndQuantity gameAndQuantity)
        {
            _context.Add(gameAndQuantity);
            return Save();
        }

        public bool Delete(Cart cart)
        {
            _context.Remove(cart);
            return Save();
        }

        public bool Delete(GameAndQuantity gameAndQuantity)
        {
            _context.Remove(gameAndQuantity);
            return Save();
        }

        public async Task<Cart?> GetCartByClientId(string clientId)
        {
            return await _context.Carts
                .Include(c => c.GamesAndQuantities)
                .ThenInclude(gq => gq.Game)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task<Game?> GetGameById(int gameId)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Cart cart)
        {
            _context.Update(cart);
            return Save();
        }

        public bool Update(GameAndQuantity gameAndQuantity)
        {
            _context.Update(gameAndQuantity);
            return Save();
        }
    }
}
