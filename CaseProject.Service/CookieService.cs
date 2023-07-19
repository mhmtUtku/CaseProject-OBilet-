using CaseProject.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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

        public void Create(string cookiename, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(1);
            options.IsEssential = true;
            options.Path = "/";

            Current.Response.Cookies.Append(cookiename, value, options);
        }

        public void Delete(string cookiename)
        {
            Current.Response.Cookies.Delete(cookiename);
        }

        public T Get<T>(string cookiename)
        {
            var cookieResult = string.Empty;

            if (Current.Request.Cookies[cookiename] != null)
                cookieResult = Current.Request.Cookies[cookiename];

            var result = JsonConvert.DeserializeObject<T>(cookieResult);

            return result;
        }
    }
}
