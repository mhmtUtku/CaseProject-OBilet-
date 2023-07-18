using Newtonsoft.Json;

namespace CaseProject.Model.Session
{
    public class UserSessionResponse : BaseResponse
    {
        [JsonProperty("data")]
        public UserSessionData Data { get; set; }
    }

    public class UserSessionData
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}
