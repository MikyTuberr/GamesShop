using shop.data;
using shop.Interfaces;
using shop.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace shop.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Game game)
        {
            _context.Add(game);
            return Save();
        }

        public bool Delete(Game game)
        {
            _context.Remove(game);
            return Save();
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        }

        public bool Update(Game game)
        {
            _context.Update(game);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public IDbAsyncEnumerator GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
