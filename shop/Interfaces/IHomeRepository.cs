using shop.Models;
using System.Data.Entity.Infrastructure;

namespace shop.Interfaces
{
    public interface IHomeRepository : IDbAsyncEnumerable
    {
        Task<IEnumerable<Game>> GetAll();

        Task<Game?> GetByIdAsync(int id);

        bool Add(Game game);

        bool Update(Game game);

        bool Delete(Game game);

        bool Save();
    }
}
