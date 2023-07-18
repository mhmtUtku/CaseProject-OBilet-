using Microsoft.AspNetCore.Http;

namespace CaseProject.Interface
{
    public interface ICookieService
    {
        void CookieCreate(string cookiename, string value);
        string CookieGet(string cookiename);
        void CookieDelete(string cookiename);
    }
}
