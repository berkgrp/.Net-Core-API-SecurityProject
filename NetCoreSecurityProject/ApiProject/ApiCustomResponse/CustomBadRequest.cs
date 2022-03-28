using System.Net;

namespace ApiProject.ApiCustomResponse
{
    public class CustomBadRequest : ApiCustomResponse
    {
        public CustomBadRequest() : base(400, HttpStatusCode.BadRequest.ToString()) //Custom Response StatusCode 400
        {
        }

        public CustomBadRequest(bool isSuccess, string message, object source) : base(400, HttpStatusCode.BadRequest.ToString(), isSuccess, message, source)
        {
        }
    }
}
