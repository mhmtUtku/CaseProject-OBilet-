using Newtonsoft.Json;

namespace CaseProject.Model.Bus
{
    public class GetBusJourneysResponse : BaseResponse
    {
        public GetBusJourneysResponse()
        {
            Data = new List<BusJourneyInfo>();
        }

        [JsonProperty("data")]
        public List<BusJourneyInfo> Data { get; set; }

        public string FormatDate { get; set; }
    }

    public class BusJourneyInfo
    {
        [JsonProperty("origin-location")]
        public string OriginLocation { get; set; }

        [JsonProperty("destination-location")]
        public string DestinationLocation { get; set; }

        [JsonProperty("journey")]
        public Journey Journey { get; set; }
    }

    public class Journey
    {
        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("departure")]
        public DateTime? Departure { get; set; }

        [JsonProperty("arrival")]
        public DateTime? Arrival { get; set; }

        [JsonProperty("original-price")]
        public decimal OriginalPrice { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
