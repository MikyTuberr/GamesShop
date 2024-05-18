using Microsoft.EntityFrameworkCore;
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

        public bool Add <T>(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Add(entity);
            return Save();
        }

        public bool Delete<T>(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
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

        public bool Update<T>(T entity)
        {
            if(entity == null)
            {
                return false;
            }
            _context.Update(entity);
            return Save();
        }
    }
}
