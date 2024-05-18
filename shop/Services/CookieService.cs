using Microsoft.AspNetCore.Http;
using shop.Interfaces;
using System;

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
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null.");
            }

            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddDays(7);

            httpContext.Response.Cookies.Append(key, value, option);
        }

        public string? GetCookie(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null.");
            }

            return httpContext.Request.Cookies[key];
        }

        public bool CookieExists(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null.");
            }

            return httpContext.Request.Cookies.ContainsKey(key);
        }

        public void DeleteCookie(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null.");
            }

            httpContext.Response.Cookies.Delete(key);
        }
    }
}
