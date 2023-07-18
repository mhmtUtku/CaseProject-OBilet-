using CaseProject.Interface;
using CaseProject.Model.Session;
using CaseProject.Models;
using Shyjus.BrowserDetection;
using System.Net;
using System.Net.Sockets;

namespace CaseProject.MiddleWare
{
    public class UserDetailsMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IUserSessionService _userSessionService;
        public UserDetailsMiddleWare(RequestDelegate next, IUserSessionService userSessionService)
        {
            _next = next;
            _userSessionService = userSessionService;
        }

        public async Task InvokeAsync(HttpContext httpContext, IBrowserDetector detector)
        {
            if (UserInfo.Current == null)
            {
                var browser = detector.Browser;

                var ipAddr = string.Empty;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddr = ip.ToString();
                    }
                }

                var apiRequest = new UserSessionRequest
                {
                    Type = 7,
                    Application = new Application { EquipmentId = "distribusion", Version = "1.0.0.0" },
                    Connection = new Connection { IpAddress = ipAddr, Port = "7082" },
                    Browser = new Browser { Name = browser.Name, Version = browser.Version }
                };

                var apiResponse = _userSessionService.GetUserSessionInfo(apiRequest);
                if(apiResponse.Status == "Success")
                {
                    UserInfo.Current = new UserSetting()
                    {
                        DeviceId = apiResponse.Data.DeviceId,
                        SessionId = apiResponse.Data.SessionId
                    };
                }
            }            

            await _next(httpContext);
        }
    }
}
