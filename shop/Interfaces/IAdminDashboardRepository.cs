using Microsoft.AspNetCore.Mvc;
using shop.Models;

namespace shop.Interfaces
{
    public interface IAdminDashboardRepository : IRepository
    {
        Task<Game?> GetGameById(int gameId);
    }
}
