using CaseProject.Helper;
using CaseProject.Interface;
using CaseProject.Model.Bus;

namespace CaseProject.Service
{
    public class BusService : IBusService
    {
        private static string token = Constant.Token;

        public GetBusJourneysResponse GetBusJourneys(GetBusJourneysRequest request)
        {
            var getBusJourneysUrl = Constant.GetBusJourneysUrl;

            Dictionary<string, string> headerParams = new Dictionary<string, string>();
            headerParams.Add("Authorization", token);

            var apiResponse = HttpHelper.HttpPost<GetBusJourneysResponse, GetBusJourneysRequest>(getBusJourneysUrl, request, headerParams);

            return apiResponse;
        }

        public GetBusLocationsResponse GetBusLocations(GetBusLocationsRequest request)
        {
            var getBusLocationsUrl = Constant.GetBusLocationsUrl;

            Dictionary<string, string> headerParams = new Dictionary<string, string>();
            headerParams.Add("Authorization", token);

            var apiResponse = HttpHelper.HttpPost<GetBusLocationsResponse, GetBusLocationsRequest>(getBusLocationsUrl, request, headerParams);

            return apiResponse;
        }
    }
}
