using Newtonsoft.Json;

namespace CaseProject.Model.Bus
{
    public class GetBusLocationsRequest : BaseRequest
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
