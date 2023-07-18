using CaseProject.Interface;
using CaseProject.Model;
using CaseProject.Model.Bus;
using CaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace CaseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusService _busService;
        private readonly ICookieService _cookieService;

        public FindJourneyRequestDataModel findParams;

        public HomeController(IBusService busService, ICookieService cookieService)
        {
            _busService = busService;
            _cookieService = cookieService;

            #region GetCookie

            var cookieVal = _cookieService.CookieGet(UserInfo.Current.CookieName);
            findParams = JsonConvert.DeserializeObject<FindJourneyRequestDataModel>(cookieVal);

            #endregion
        }

        public IActionResult Index()
        {
            return View(findParams);
        }

        [HttpPost]
        public JsonResult GetBusLocations(string term)
        {
            var request = new GetBusLocationsRequest
            {
                Data = term,
                DeviceSession = new DeviceSession { DeviceId = UserInfo.Current.DeviceId, SessionId = UserInfo.Current.SessionId }
            };

            var busLocations = _busService.GetBusLocations(request);

            return Json(busLocations);
        }

        public IActionResult FindJourney(int fromWhereId, int toWhereId, string date, string fromWhere, string toWhere)
        {
            DateTime departureDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out departureDate);
            }

            #region Validation

            if(fromWhereId == toWhereId)
            {
                var model = new ErrorViewModel
                {
                    ErrorMessage = "Hem çıkış hem de varış noktası olarak aynı konum seçilemez."
                };
                return View("Error", model);
            }

            if(departureDate < DateTime.Now.Date)
            {
                var model = new ErrorViewModel
                {
                    ErrorMessage = "Hareket tarihi için minimum geçerlilik tarihi Bugün'dür."
                };
                return View("Error", model);
            }

            #endregion

            var request = new GetBusJourneysRequest
            {
                Data = new Data { OriginId = fromWhereId, DestinationId = toWhereId, DepartureDate = departureDate },
                DeviceSession = new DeviceSession { DeviceId = UserInfo.Current.DeviceId, SessionId = UserInfo.Current.SessionId }
            };

            var busJourneys = _busService.GetBusJourneys(request);
            if(busJourneys.Status == "Success")
            {
                busJourneys.FormatDate = departureDate.ToString("dd MMMM dddd");

                #region CreateCookie

                var cookieModel = new FindJourneyRequestDataModel
                {
                    FromWhereId = fromWhereId,
                    FromWhere = fromWhere,
                    ToWhere = toWhere,
                    ToWhereId = toWhereId,
                    Date = departureDate.ToString("dd MMMM yyyy dddd")
            };
                var cookieVal = JsonConvert.SerializeObject(cookieModel);

                _cookieService.CookieCreate(UserInfo.Current.CookieName, cookieVal);

                #endregion

                busJourneys.Data = busJourneys.Data.OrderBy(p => p.Journey.Departure).ToList();
            }

            return View(busJourneys);
        }
    }
}