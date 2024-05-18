using shop.data;
using shop.Interfaces;
using shop.Models;
using Microsoft.EntityFrameworkCore;

namespace shop.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Add<T>(T entity)
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


        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update<T>(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Update(entity);
            return Save();
        }
    }
}
