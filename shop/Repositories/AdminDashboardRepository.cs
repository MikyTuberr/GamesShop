using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.data;
using shop.Interfaces;
using shop.Models;

namespace shop.Repositories
{
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        ApplicationDbContext _context;
        public AdminDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Game?> GetGameById(int gameId)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
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
