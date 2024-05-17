using Azure;
using shop.Interfaces;
using shop.Models;

namespace shop.Services
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string key, string value, int? expireTime = null)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddDays(7);

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }

        public string GetCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public bool CookieExists(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key);
        }

        public void DeleteCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
    }
}
