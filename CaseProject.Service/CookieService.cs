using CaseProject.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CaseProject.Service
{
    public class CookieService : ICookieService
    {
        private IServiceProvider _serviceProvider;

        public CookieService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private IHttpContextAccessor _accessor { get { return _serviceProvider.GetRequiredService<IHttpContextAccessor>(); } }
        public HttpContext Current => _accessor.HttpContext;

        public void CookieCreate(string cookiename, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(1);
            options.IsEssential = true;
            options.Path = "/";

            Current.Response.Cookies.Append(cookiename, value, options);
        }

        public void CookieDelete(string cookiename)
        {
            Current.Response.Cookies.Delete(cookiename);
        }

        public string CookieGet(string cookiename)
        {
            var result = string.Empty;

            if (Current.Request.Cookies[cookiename] != null)
                result = Current.Request.Cookies[cookiename];

            return result;
        }
    }
}
