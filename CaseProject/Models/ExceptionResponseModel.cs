using Newtonsoft.Json;

namespace CaseProject.UI.Models
{
    public class ExceptionResponseModel
    {
        public ExceptionResponseModel(string message, int statusCode = 500)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
    }
}
