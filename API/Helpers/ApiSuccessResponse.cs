using Newtonsoft.Json;

namespace API.Helpers
{
    public class ApiSuccessResponse
    {
        public object Data { get; }
        public int StatusCode { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
        public ApiSuccessResponse(int statusCode, object data, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Data = data;
        }
        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "Sucesso";
                default:
                    return null;
            }
        }
    }
}