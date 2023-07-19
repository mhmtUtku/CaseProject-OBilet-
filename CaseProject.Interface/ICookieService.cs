using Microsoft.AspNetCore.Http;

namespace CaseProject.Interface
{
    public interface ICookieService
    {
        void Create(string cookiename, string value);
        T Get<T>(string cookiename);
        void Delete(string cookiename);
    }
}
