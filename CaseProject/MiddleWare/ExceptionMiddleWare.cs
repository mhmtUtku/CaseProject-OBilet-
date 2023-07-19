using CaseProject.UI.Models;
using Newtonsoft.Json;

namespace CaseProject.UI.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string message = "";
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                var response = new ExceptionResponseModel(message);
                var json = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
