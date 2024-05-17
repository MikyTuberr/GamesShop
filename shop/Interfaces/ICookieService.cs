using shop.Models;
using System.Data.Entity.Infrastructure;

namespace shop.Interfaces
{
    public interface ICookieService
    {
        void SetCookie(string key, string value, int? expireTime = null);
        string GetCookie(string key);
        void DeleteCookie(string key);
        bool CookieExists(string key);
    }
}
