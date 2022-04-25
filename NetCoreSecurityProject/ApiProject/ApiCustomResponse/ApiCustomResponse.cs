using Newtonsoft.Json;

namespace ApiProject.ApiCustomResponse
{
    public class ApiCustomResponse
    {
        // we are generating our custom response of that api.
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public bool IsSuccess { get; private set; }

        public object Data { get; private set; }

        public ApiCustomResponse(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiCustomResponse(int statusCode, string statusDescription, bool isSuccess, string message, object source)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
            this.IsSuccess = isSuccess;
            this.Data = source;
        }
    }
}
