using shop.Models;

namespace shop.Interfaces
{
    public interface IHomeRepository : IRepository
    {
        Task<IEnumerable<Game>> GetAll();

        Task<Game?> GetByIdAsync(int id);
    }
}
