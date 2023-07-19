using CaseProject.Helper;
using CaseProject.Interface;
using CaseProject.Model.Bus;

namespace CaseProject.Service
{
    public class BusService : IBusService
    {
        private static string token = Constant.Token;

        public async Task<GetBusJourneysResponse> GetBusJourneys(GetBusJourneysRequest request)
        {
            var getBusJourneysUrl = Constant.GetBusJourneysUrl;

            Dictionary<string, string> headerParams = new Dictionary<string, string>();
            headerParams.Add("Authorization", token);

            var apiResponse = await HttpHelper.HttpPost<GetBusJourneysResponse, GetBusJourneysRequest>(getBusJourneysUrl, request, headerParams);

            return apiResponse;
        }

        public async Task<GetBusLocationsResponse> GetBusLocations(GetBusLocationsRequest request)
        {
            var getBusLocationsUrl = Constant.GetBusLocationsUrl;

            Dictionary<string, string> headerParams = new Dictionary<string, string>();
            headerParams.Add("Authorization", token);

            var apiResponse = await HttpHelper.HttpPost<GetBusLocationsResponse, GetBusLocationsRequest>(getBusLocationsUrl, request, headerParams);

            return apiResponse;
        }
    }
}
